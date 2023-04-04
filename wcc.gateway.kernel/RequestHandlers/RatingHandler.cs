﻿using AutoMapper;
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
            var ratingDto = _db.GetRating(request.Id);
            if (ratingDto == null)
                throw new Exception("Rating does not exist.");

            var players = _db.GetPlayers();
            
            var rating = new List<RatingModel>();
            foreach (var rp in ratingDto)
            {
                var player = players.FirstOrDefault(p => p.Id == rp.PlayersId);
                
                rating.Add(new RatingModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    AvatarUrl = DiscordHelper.GetAvatarUrl(player.User.ExternalId, player.User.Avatar)
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
