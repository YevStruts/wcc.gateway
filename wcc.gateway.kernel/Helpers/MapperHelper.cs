using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.integrations.Discord.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.C3;
using wcc.gateway.kernel.Models.Game;
using wcc.gateway.kernel.Models.Player;
using wcc.gateway.kernel.Models.User;
using Core = wcc.gateway.kernel.Models.Core;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.Helpers
{
    public sealed class MapperHelper
    {
        private static IMapper? instance = null;
        
        private MapperHelper()
        {
        }

        public static IMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<News, NewsModel>()
                            .ForMember(dest => dest.Image_url, act => act.MapFrom(src => src.ImageUrl));

                        cfg.CreateMap<Country, CountryModel>();

                        cfg.CreateMap<Player, PlayerModelOld>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<Player, PlayerModel>();

                        cfg.CreateMap<Core.PlayerModel, PlayerModel>();

                        cfg.CreateMap<Core.GameModel, GameModel>();

                        cfg.CreateMap<Team, PlayerModelOld>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => string.Empty));

                        cfg.CreateMap<Player, PlayerGameListModel>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<Team, PlayerGameListModel>()
                                .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => string.Empty));

                        cfg.CreateMap<Tournament, TournamentModelOld>()
                            .ForMember(dest => dest.Image_url, act => act.MapFrom(src => src.ImageUrl))
                            .ForMember(dest => dest.Count_players, act => act.MapFrom(src => src.CountPlayers))
                            .ForMember(dest => dest.Date_created, act => act.MapFrom(src => src.DateCreated))
                            .ForMember(dest => dest.Date_start, act => act.MapFrom(src => src.DateStart));

                        cfg.CreateMap<Core.TournamentModel, TournamentModel>().ReverseMap();

                        cfg.CreateMap<List<PlayerModelOld>, IEnumerable<PlayerModelOld>>();

                        cfg.CreateMap<Game, GameModelOld>()
                            .ForMember(dest => dest.Scheduled, act => act.MapFrom(src => ((DateTimeOffset)(src.Scheduled)).ToUnixTimeMilliseconds()));

                        cfg.CreateMap<Game, GameListModelOld>()
                            .ForMember(dest => dest.Scheduled, act => act.MapFrom(src => ((DateTimeOffset)(src.Scheduled)).ToUnixTimeMilliseconds()));

                        cfg.CreateMap<User, WhoAmIModel>()
                            .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role.Name));

                        cfg.CreateMap<User, UserModel>();

                        cfg.CreateMap<Player, PlayerProfile>()
                            .ForMember(dest => dest.Avatar, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<IEnumerable<Game>, IEnumerable<LastFightsList>>();

                        cfg.CreateMap<Player, C3UserItemModel>()
                            .ForMember(dest => dest.id, act => act.MapFrom(src => src.Id))
                            .ForMember(dest => dest.nickname, act => act.MapFrom(src => src.Name))
                            .ForMember(dest => dest.token, act => act.MapFrom(src => src.Token));

                        cfg.CreateMap<wcc.gateway.kernel.Models.RatingGame.RatingGame1x1Model, Microservices.Rating.RatingGame.RatingGame1x1Model>().ReverseMap();
                        cfg.CreateMap<wcc.gateway.kernel.Models.RatingGame.RatingGameSettings1x1Model, Microservices.Rating.RatingGame.RatingGameSettings1x1Model>().ReverseMap();
                    });

                    instance = new Mapper(config);
                }
                return instance;
            }
        }
    }
}
