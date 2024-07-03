using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.integrations.Discord.Helpers;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Rating = wcc.gateway.kernel.Models.Rating;
using Core = wcc.gateway.kernel.Models.Core;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetRatingQuery : IRequest<List<RatingModel>>
    {
        public long Id { get; }

        public string Locale { get; }

        public GetRatingQuery(long id, string locale)
        {
            Id = id;
            Locale = locale;
        }
    }

    public class UpdateRatingQuery : IRequest<bool>
    {

    }

    public class RatingHandler : IRequestHandler<GetRatingQuery, List<RatingModel>>,
        IRequestHandler<UpdateRatingQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public RatingHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<List<RatingModel>> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            var players = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.PlayerModel>>("api/player");

            var playersSql = _db.GetPlayers();

            var users = _db.GetUsers();

            var countries = _db.GetCountries();

            var playerData = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<List<PlayerData>>("api/rating");

            var rating = new List<RatingModel>();
            if (playerData != null && playerData.Count > 0)
            {
                int position = 0;
                foreach (var rp in playerData.OrderByDescending(r => r.Points).ToList())
                {
                    var player = players.FirstOrDefault(p => p.Id == rp.PlayerId);
                    if (player != null && player.Name != null && player.IsActive)
                    {
                        var user = users.First(u => u.Id.ToString() == player.UserId);

                        var playerSql = playersSql.First(p => p.UserId.ToString() == player.UserId);

                        var nation = countries.FirstOrDefault(c => c.Id == playerSql.CountryId);

                        rating.Add(new RatingModel
                        {
                            Id = player.Id,
                            Name = player.Name,
                            AvatarUrl = DiscordHelper.GetAvatarUrl(user.ExternalId, user.Avatar),
                            Position = position++,
                            Progress = 0,
                            TotalPoints = rp.Points,
                            Nation = nation?.Code ?? "xx"
                        });
                    }
                }

                var language = _db.GetLanguage(request.Locale);
                if (language != null)
                {
                    //var translation = ratingDto.Translations.FirstOrDefault(t => t.LanguageId == language.Id);
                    //if (translation != null)
                    //{
                    //    rating.Name = translation.Name;
                    //}
                }
            }
            return rating;
        }

        public async Task<bool> Handle(UpdateRatingQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers();

            var countries = _db.GetCountries();

            var playerData = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<List<PlayerData>>("api/rating");

            var rating = new List<Rating.RatingModel>();
            if (playerData != null && playerData.Count > 0)
            {
                int position = 0;
                foreach (var rp in playerData.OrderByDescending(r => r.Points).ToList())
                {
                    var player = players.FirstOrDefault(p => p.Id.ToString() == rp.PlayerId);
                    if (player != null && player.Name != null && player.IsActive)
                    {
                        var nation = countries.FirstOrDefault(c => c.Id == player.CountryId);

                        rating.Add(new Rating.RatingModel
                        {
                            PlayerId = rp.PlayerId,
                            Points = rp.Points
                        });
                    }
                }
            }

            return await new ApiCaller(_mcsvcConfig.RatingUrl).PostAsync<List<Rating.RatingModel>, bool>("api/rating", rating);
        }
    }
}
