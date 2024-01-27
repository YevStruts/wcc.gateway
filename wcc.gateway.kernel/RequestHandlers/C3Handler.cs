using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models.C3;
using wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class LoginQuery : IRequest<C3PlayerModel>
    {
        public string Token { get; }
        public LoginQuery(string token)
        {
            Token = token;
        }
    }

    public class C3GetRatingQuery : IRequest<C3RatingModel>
    {
        public C3GetRatingQuery()
        {
        }
    }

    public class GameResultQuery : IRequest<bool>
    {
        public List<GameResultModel> Result { get; }
        public GameResultQuery(List<GameResultModel> result)
        {
            Result = result;
        }
    }

    public class C3Handler : IRequestHandler<LoginQuery, C3PlayerModel>,
        IRequestHandler<C3GetRatingQuery, C3RatingModel>,
        IRequestHandler<GameResultQuery, bool>

    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly RatingConfig _ratingConfig;

        public C3Handler(IDataRepository db, RatingConfig ratingConfig)
        {
            _db = db;
            _ratingConfig = ratingConfig;
        }

        public async Task<C3PlayerModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var model = new C3PlayerModel { nickname = "Anonymous", result = false };
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                    throw new Exception();

                var player = _db.GetPlayers().FirstOrDefault(p => p.Token == request.Token);
                if (!(player == null || string.IsNullOrEmpty(player.Name)))
                {
                    model.id = player.Id;
                    model.result = true;
                    model.nickname = player.Name;
                }
            }
            catch (Exception ex)
            {
            }
            return model;
        }

        public async Task<C3RatingModel> Handle(C3GetRatingQuery request, CancellationToken cancellationToken)
        {
            var rating = await new ApiCaller(_ratingConfig.Url).GetAsync<List<C3RankModel>>("api/C3/Rating");

            var players = _db.GetPlayers().Where(p => p.IsActive).ToList();

            var model = new C3RatingModel() { result = true };
            players.ForEach(p =>
            {
                var pRank = rating.FirstOrDefault(r => r.PlayerId == p.Id);
                if (pRank != null)
                {
                    model.players.Add(new C3RatingItemModel
                    {
                        id = p.Id,
                        name = p.Name ?? string.Empty,
                        score = pRank.Score
                    });
                }
            });

            return model;
        }

        public async Task<bool> Handle(GameResultQuery request, CancellationToken cancellationToken)
        {
            if (request.Result.Count > 0)
            {
                var response = await new ApiCaller(_ratingConfig.Url).PostAsync<List<GameResultModel>, string>("api/C3/Save", request.Result);

                return true;
            }
            return false;
        }
    }
}
