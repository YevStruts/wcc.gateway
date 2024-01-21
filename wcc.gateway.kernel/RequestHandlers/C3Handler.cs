using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models.C3;

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

    public class GameResultQuery : IRequest<bool>
    {
        public List<GameResultModel> Result { get; }
        public GameResultQuery(List<GameResultModel> result)
        {
            Result = result;
        }
    }

    public class C3Handler : IRequestHandler<LoginQuery, C3PlayerModel>,
        IRequestHandler<GameResultQuery, bool>

    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public C3Handler(IDataRepository db)
        {
            _db = db;
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

        public Task<bool> Handle(GameResultQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}
