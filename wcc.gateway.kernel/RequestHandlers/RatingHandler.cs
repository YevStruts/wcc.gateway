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
            var ratingDto = _db.GetRating(request.Id);
            if (ratingDto == null)
                throw new Exception("Rating does not exist.");

            //var rating = _mapper.Map<List<RatingModel>>(ratingDto);

            var rank = new[] { 41,44, 28,45, 60,39, 24,10, 47,19, 50,52, 20,21, 26,5 , 3 ,8 , 65,53, 51,38, 9 ,35, 48,29, 13,37, 40,11, 56,33, 16,32, 25,7 , 42,4 , 12,18, 54, 2 };
            var players = _db.GetPlayers();


            var rating = new List<RatingModel>();

            foreach (var id in rank)
            {
                var player = players.FirstOrDefault(p => p.Id == id);
                
                rating.Add(new RatingModel { Id = player.Id, Name = player.Name,
                    AvatarUrl = $"https://cdn.discordapp.com/avatars/{player.User.ExternalId}/{player.User.Avatar}.png" });
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
