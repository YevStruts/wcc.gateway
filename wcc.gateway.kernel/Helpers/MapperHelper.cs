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

                        cfg.CreateMap<Player, PlayerModel>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<Team, PlayerModel>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => string.Empty));

                        cfg.CreateMap<Player, PlayerGameListModel>()
                            .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<Team, PlayerGameListModel>()
                                .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => string.Empty));

                        cfg.CreateMap<Tournament, TournamentModel>()
                            .ForMember(dest => dest.Image_url, act => act.MapFrom(src => src.ImageUrl))
                            .ForMember(dest => dest.Count_players, act => act.MapFrom(src => src.CountPlayers))
                            .ForMember(dest => dest.Date_created, act => act.MapFrom(src => src.DateCreated))
                            .ForMember(dest => dest.Date_start, act => act.MapFrom(src => src.DateStart));

                        cfg.CreateMap<List<PlayerModel>, IEnumerable<PlayerModel>>();

                        cfg.CreateMap<Game, GameModel>()
                            .ForMember(dest => dest.Scheduled, act => act.MapFrom(src => ((DateTimeOffset)(src.Scheduled)).ToUnixTimeMilliseconds()));

                        cfg.CreateMap<Game, GameListModel>()
                            .ForMember(dest => dest.Scheduled, act => act.MapFrom(src => ((DateTimeOffset)(src.Scheduled)).ToUnixTimeMilliseconds()));

                        cfg.CreateMap<User, WhoAmIModel>()
                            .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role.Name));

                        cfg.CreateMap<Player, PlayerProfile>()
                            .ForMember(dest => dest.Avatar, act => act.MapFrom(src => DiscordHelper.GetAvatarUrl(src.User.ExternalId, src.User.Avatar)));

                        cfg.CreateMap<IEnumerable<Game>, IEnumerable<LastFightsList>>();
                    });

                    instance = new Mapper(config);
                }
                return instance;
            }
        }
    }
}
