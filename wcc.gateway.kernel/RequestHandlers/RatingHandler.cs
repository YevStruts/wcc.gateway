using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.integrations.Discord.Helpers;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Microservices;

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

    public class RatingHandler : IRequestHandler<GetRatingQuery, List<RatingModel>>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly RatingConfig _ratingConfig;

        public RatingHandler(IDataRepository db, RatingConfig ratingConfig)
        {
            _db = db;
            _ratingConfig = ratingConfig;
        }

        public async Task<List<RatingModel>> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers();

            var playerData = await new ApiCaller(_ratingConfig.Url).GetAsync<List<PlayerData>>("api/rating");

            var rating = new List<RatingModel>();
            if (playerData != null && playerData.Count > 0)
            {
                int position = 0;
                foreach (var rp in playerData.OrderByDescending(r => r.Points).ToList())
                {
                    var player = players.FirstOrDefault(p => p.Id == rp.PlayerId);
                    if (player != null && player.Name != null)
                    {
                        rating.Add(new RatingModel
                        {
                            Id = player.Id,
                            Name = player.Name,
                            AvatarUrl = DiscordHelper.GetAvatarUrl(player.User.ExternalId, player.User.Avatar),
                            Position = position++,
                            Progress = 0,
                            TotalPoints = rp.Points
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
    }
}
