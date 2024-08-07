﻿using AutoMapper;
using MediatR;
using System.Net;
using System.Text;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models.C3;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class LoginQuery : IRequest<C3PlayerModel>
    {
        public string Token { get; }
        public LoginQuery(string token)
        {
            Token = token;
        }
    }
    
    public class C3GetUsersQuery : IRequest<C3UsersModel>
    {
        public C3GetUsersQuery()
        {
        }
    }

    public class C3GetRatingQuery : IRequest<C3RatingModel>
    {
        public int Id { get; private set; }
        public C3GetRatingQuery(int id)
        {
            Id = id;
        }
    }

    public class GameResultQuery : IRequest<bool>
    {
        public C3GameResultModel Result { get; }
        public GameResultQuery(C3GameResultModel result)
        {
            Result = result;
        }
    }

    public class C3Handler : IRequestHandler<LoginQuery, C3PlayerModel>,
        IRequestHandler<C3GetUsersQuery, C3UsersModel>,
        IRequestHandler<C3GetRatingQuery, C3RatingModel>,
        IRequestHandler<GameResultQuery, bool>

    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public C3Handler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<C3PlayerModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var model = new C3PlayerModel { nickname = "Anonymous" };
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                    throw new Exception();

                var player = _db.GetPlayers().FirstOrDefault(p => p.Token == request.Token);
                if (!(player == null || string.IsNullOrEmpty(player.Name)))
                {
                    model.id = player.Id;
                    model.nickname = player.Name;
                }
            }
            catch (Exception ex)
            {
            }
            return model;
        }

        public async Task<C3UsersModel> Handle(C3GetUsersQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers().Where(p => p.IsActive && !string.IsNullOrEmpty(p.Token)).ToList();

            var model = new C3UsersModel() { result = true };
            model.users = _mapper.Map<List<C3UserItemModel>>(players);            
            return model;
        }

        public async Task<C3RatingModel> Handle(C3GetRatingQuery request, CancellationToken cancellationToken)
        {
            var rating = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<C3RankModel>($"api/C3/Rating/{request.Id}");

            var players = _db.GetPlayers().Where(p => p.IsActive).ToList();

            var model = new C3RatingModel() { result = true };
            players.ForEach(p =>
            {
                var pRank = rating.Items.FirstOrDefault(r => r.PlayerId == p.Id);
                if (pRank != null)
                {
                    model.players.Add(new C3RatingItemModel
                    {
                        id = p.Id,
                        name = p.Name ?? string.Empty,
                        score = pRank.Score,
                        games = p.Statistic?.Games ?? 0,
                        wins = p.Statistic?.Wins ?? 0
                    });
                }
            });

            return model;
        }

        public async Task<bool> Handle(GameResultQuery request, CancellationToken cancellationToken)
        {
            if (request.Result.Items.Count > 0)
            {
                var playerIds = request.Result.Items.Select(r => r.player_id).ToArray();
                var players = _db.GetPlayers().Where(p => playerIds.Contains(p.Id)).ToList();

                if (isRatingGame(request.Result.Description))
                {
                    // calc rating
                    var result = await new ApiCaller(_mcsvcConfig.RatingUrl)
                        .PostAsync<C3GameResultModel, List<C3SaveRankModel>>("api/C3/Save", request.Result);

                    foreach (var item in request.Result.Items)
                    {
                        var player = players.FirstOrDefault(p => p.Id == item.player_id);
                        if (player != null)
                        {
                            if (player.Statistic == null)
                                player.Statistic = new Statistic { PlayerId = item.player_id, Wins = 0, Games = 0 };

                            player.Statistic.Games++;
                            if (item.result == (int)GameResult.WIN)
                                player.Statistic.Wins++;
                            _db.UpdatePlayer(player);
                        }
                    }

                    // post results
                    var payload = CommonHelper.CreateGameResultPayload(players, request.Result.Items, result);

                    WebClient client = new WebClient();
                    client.Headers.Add("Content-Type", "application/json");
                    client.UploadData("https://discord.com/api/webhooks/1189043276708327504/oLsVBHxSZ9b5TIPdIaKC__stUNz5Gby9xlapGeLkZLv2uvlrTSsU__P1I0wATxyqi8kc",
                        Encoding.UTF8.GetBytes(payload));
                }

                return true;
            }
            return false;
        }

        private bool isRatingGame(string? description)
        {
            // TODO: Check description do we need to save game.
            // request.Result.Description
            //auto description = it->first;
            //if ((WCCHelper::StartsWith(description, "\"qs=") ||
            //    WCCHelper::StartsWith(description, "\"qz=") ||
            //    WCCHelper::StartsWith(description, "\"qp=") ||
            //    WCCHelper::StartsWith(description, "\"qt=")) &&
            //    WCCHelper::Contains(description, "0000000000"))
            //{
            //}

            if (string.IsNullOrEmpty(description))
                return false;

            return (description.StartsWith("\"qs=") ||
                    description.StartsWith("\"qz=") ||
                    description.StartsWith("\"qp=") ||
                    description.StartsWith("\"qt=")) &&
                    description.Contains("0000000000");
                    
        }
    }
}
