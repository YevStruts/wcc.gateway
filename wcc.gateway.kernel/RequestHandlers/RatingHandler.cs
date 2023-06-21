using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.integrations.Discord.Helpers;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

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

        public RatingHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<List<RatingModel>> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            //var ratingDto = _db.GetRating(request.Id);
            //if (ratingDto == null)
            //    throw new Exception("Rating does not exist.");

            List<PlayerRatingView> ratingDto = new List<PlayerRatingView> {
                new PlayerRatingView() { PlayersId = 44, Position =  1, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 28, Position =  2, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 45, Position =  3, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 70, Position =  4, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 24, Position =  5, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 60, Position =  6, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 39, Position =  7, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 50, Position =  8, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 52, Position =  9, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 63, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 47, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 20, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 71, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  5, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 10, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 65, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 21, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  3, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 26, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 51, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 19, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  8, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 48, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 53, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 38, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 66, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 29, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  1, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  9, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 95, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 16, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 40, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 11, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 13, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 79, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 68, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 56, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 35, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 37, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 78, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 32, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 80, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 33, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 25, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 42, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  4, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 75, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 74, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 88, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  7, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 54, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 18, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 12, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 81, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  2, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 82, Position = 10, Progress = 0, RatingId = 3, TotalPoints = 0 },
            };

            var players = _db.GetPlayers();
            
            var rating = new List<RatingModel>();
            foreach (var rp in ratingDto)
            {
                var player = players.FirstOrDefault(p => p.Id == rp.PlayersId);
                
                rating.Add(new RatingModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    AvatarUrl = DiscordHelper.GetAvatarUrl(player.User.ExternalId, player.User.Avatar),
                    Position = rp.Position,
                    Progress = rp.Progress,
                    TotalPoints = rp.TotalPoints
                });
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

            return Task.FromResult(rating);
        }
    }
}
