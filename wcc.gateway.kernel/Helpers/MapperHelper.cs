﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Infrastructure;
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
                        
                        cfg.CreateMap<Player, PlayerModel>();

                        cfg.CreateMap<Tournament, TournamentModel>()
                            .ForMember(dest => dest.Image_url, act => act.MapFrom(src => src.ImageUrl))
                            .ForMember(dest => dest.Count_players, act => act.MapFrom(src => src.CountPlayers))
                            .ForMember(dest => dest.Date_created, act => act.MapFrom(src => src.DateCreated))
                            .ForMember(dest => dest.Date_start, act => act.MapFrom(src => src.DateStart));
                    });

                    instance = new Mapper(config);
                }
                return instance;
            }
        }
    }
}