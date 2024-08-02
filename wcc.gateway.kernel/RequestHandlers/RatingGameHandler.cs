using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Web;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.RatingGame;
using wcc.gateway.kernel.Models.User;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetRatingGamesQuery : IRequest<List<RatingGame1x1Model>>
    {
    }

    public class GetRatingGameQuery : IRequest<RatingGame1x1Model>
    {
        public string RatingGameId { get; private set; }
        public GetRatingGameQuery(string ratingGameId)
        {
            RatingGameId = ratingGameId;
        }
    }

    public class SaveOrUpdateRatingGameQuery : IRequest<bool>
    {
        public RatingGame1x1Model Game { get; private set; }
        public SaveOrUpdateRatingGameQuery(RatingGame1x1Model game)
        {
            Game = game;
        }
    }

    public class DeleteRatingGameQuery : IRequest<bool>
    {
        public string Id { get; private set; }
        public DeleteRatingGameQuery(string id)
        {
            Id = id;
        }
    }

    public class RatingGameHandler : IRequestHandler<GetRatingGamesQuery, List<RatingGame1x1Model>>,
        IRequestHandler<GetRatingGameQuery, RatingGame1x1Model>,
        IRequestHandler<SaveOrUpdateRatingGameQuery, bool>,
        IRequestHandler<DeleteRatingGameQuery, bool>
    {
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly ILogger<RatingGameHandler> _logger;
        private readonly Microservices.Config _mcsvcConfig;

        public RatingGameHandler(ILogger<RatingGameHandler> logger, Microservices.Config mcsvcConfig)
        {
            _logger = logger;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<List<RatingGame1x1Model>> Handle(GetRatingGamesQuery request, CancellationToken cancellationToken)
        {
            var ratingGames = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<List<Microservices.Rating.RatingGame.RatingGame1x1Model>>($"api/ratinggame");
            List<RatingGame1x1Model> model = new List<RatingGame1x1Model>();
            ratingGames.ForEach(r => model.Add(_mapper.Map<RatingGame1x1Model>(r)));
            return model;
        }

        public async Task<RatingGame1x1Model> Handle(GetRatingGameQuery request, CancellationToken cancellationToken)
        {
            string ratingGameId = HttpUtility.UrlEncode(request.RatingGameId);
            var ratingGame = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<Microservices.Rating.RatingGame.RatingGame1x1Model>($"api/ratinggame/{ratingGameId}");
            return _mapper.Map<RatingGame1x1Model>(ratingGame);
        }

        public async Task<bool> Handle(SaveOrUpdateRatingGameQuery request, CancellationToken cancellationToken)
        {
            var game = _mapper.Map<Microservices.Rating.RatingGame.RatingGame1x1Model>(request.Game);
            return await new ApiCaller(_mcsvcConfig.RatingUrl).PostAsync<Microservices.Rating.RatingGame.RatingGame1x1Model, bool>($"api/ratinggame", game);
        }

        public async Task<bool> Handle(DeleteRatingGameQuery request, CancellationToken cancellationToken)
        {
            string requestId = HttpUtility.UrlEncode(request.Id);
            return await new ApiCaller(_mcsvcConfig.RatingUrl).DeleteAsync($"api/ratinggame/{requestId}");
        }
    }
}
