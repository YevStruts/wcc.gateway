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
                new PlayerRatingView() { PlayersId = 63, Position = 11, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 47, Position = 12, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 20, Position = 13, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 71, Position = 14, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  5, Position = 15, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 10, Position = 16, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 65, Position = 17, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 21, Position = 18, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  3, Position = 19, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 26, Position = 20, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 51, Position = 21, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 19, Position = 22, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  8, Position = 23, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 48, Position = 24, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 53, Position = 25, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 38, Position = 26, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 66, Position = 27, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 29, Position = 28, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  1, Position = 29, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  9, Position = 30, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 95, Position = 31, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 16, Position = 32, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 40, Position = 33, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 11, Position = 34, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 13, Position = 35, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 79, Position = 36, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 68, Position = 37, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 56, Position = 38, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 35, Position = 39, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 37, Position = 40, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 78, Position = 41, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 32, Position = 42, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 80, Position = 43, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 33, Position = 44, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 25, Position = 45, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 42, Position = 46, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  4, Position = 47, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 75, Position = 48, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 74, Position = 49, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 88, Position = 50, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  7, Position = 51, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 54, Position = 52, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 18, Position = 53, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 12, Position = 54, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 81, Position = 55, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId =  2, Position = 56, Progress = 0, RatingId = 3, TotalPoints = 0 },
                new PlayerRatingView() { PlayersId = 82, Position = 57, Progress = 0, RatingId = 3, TotalPoints = 0 },
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
