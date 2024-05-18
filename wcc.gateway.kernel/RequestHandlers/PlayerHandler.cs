using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Text.Json;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Player;
using Core = wcc.gateway.kernel.Models.Core;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetPlayerByUserIdQuery : IRequest<PlayerModel>
    {
        public string UserId { get; }
        public GetPlayerByUserIdQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }

    public class GetPlayersQuery : IRequest<IEnumerable<PlayerModel>>
    {

    }

    public class GetPlayerDetailQuery : IRequest<PlayerModelOld>
    {
        public long Id { get; }

        public GetPlayerDetailQuery(long id)
        { 
            Id = id;
        }
    }

    public class GetPlayerListQuery : IRequest<IEnumerable<PlayerModelOld>>
    {
    }

    public class GetPlayerProfileQuery : IRequest<PlayerProfile>
    {
        public long Id { get; }

        public GetPlayerProfileQuery(long id)
        {
            Id = id;
        }
    }

    public class UpdatePlayerQuery : IRequest<bool>
    {
        public PlayerModelOld Player { get; }
        public string ExternalUserId { get; }

        public UpdatePlayerQuery(PlayerModelOld player, string userId)
        {
            Player = player;
            ExternalUserId = userId;
        }
    }

    public class RegisterPlayerQuery : IRequest<bool>
    {
        public RegisterPlayerModel Player { get; }

        public RegisterPlayerQuery(RegisterPlayerModel player)
        {
            Player = player;
        }
    }

    public class PlayerHandler :
        IRequestHandler<GetPlayerByUserIdQuery, PlayerModel>,
        IRequestHandler<GetPlayersQuery, IEnumerable<PlayerModel>>,
        IRequestHandler<GetPlayerDetailQuery, PlayerModelOld>,
        IRequestHandler<GetPlayerListQuery, IEnumerable<PlayerModelOld>>,
        IRequestHandler<GetPlayerProfileQuery, PlayerProfile>,
        IRequestHandler<UpdatePlayerQuery, bool>,
        IRequestHandler<RegisterPlayerQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly ILogger<PlayerHandler> _logger;
        private readonly Microservices.Config _mcsvcConfig;

        public PlayerHandler(IDataRepository db, ILogger<PlayerHandler> logger, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _logger = logger;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<PlayerModel> Handle(GetPlayerByUserIdQuery request, CancellationToken cancellationToken)
        {
            PlayerModel tmp = null;
            try
            {
                var player = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<Core.PlayerModel>($"api/player/ByUserId/{request.UserId}");
                tmp = _mapper.Map<PlayerModel>(player);
            }
            catch (Exception e)
            {

            }
            return tmp;
        }

        public async Task<IEnumerable<PlayerModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.PlayerModel>>($"api/player");
            var model = _mapper.Map<IEnumerable<PlayerModel>>(players);
            return model;
        }

        public Task<PlayerModelOld> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {
            var player = _db.GetPlayer(request.Id);
            var model = _mapper.Map<PlayerModelOld>(player);
            return Task.FromResult(model);
        }

        public Task<IEnumerable<PlayerModelOld>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers();
            var model = _mapper.Map<IEnumerable<PlayerModelOld>>(players);
            return Task.FromResult(model);
        }

        public async Task<PlayerProfile> Handle(GetPlayerProfileQuery request, CancellationToken cancellationToken)
        {
            var games = _db.GetLastFightsStatistics(request.Id, 1);

            var playersDto = _db.GetPlayers();
            var playerDto = playersDto.First(p => p.Id == request.Id);

            var model = _mapper.Map<PlayerProfile>(playerDto);
            model.Debut = games.OrderBy(g => g.Date).FirstOrDefault()?.Date ?? DateTime.MinValue;
            model.LastFight = games.OrderByDescending(g => g.Date).FirstOrDefault()?.Date ?? DateTime.MinValue;
            model.LastFightsList = new List<LastFightsList>();

            int wins = 0;
            int losses = 0;
            foreach (var game in games)
            {
                if (!game.GameName?.Contains("(TL)") ?? false)
                {
                    List<string> last6Fights = new List<string>();
                    if (game.LastFights != null)
                    {
                        foreach (var lastFight in game.LastFights.Split(','))
                        {
                            var fightCode = "#888";
                            if (lastFight.Trim() == "1")
                                fightCode = "#080";
                            else if (lastFight.Trim() == "-1")
                                fightCode = "#800";

                            last6Fights.Add(fightCode);
                        }
                    }

                    model.LastFightsList.Add(new LastFightsList
                    {
                        Date = game.Date,
                        Name = game.Name,
                        Wins = game.Wins ?? 0,
                        Losses = game.Losses ?? 0,
                        Last6 = last6Fights,
                        Tournament = game.Tournament,
                        Wld = game.Result ?? 0
                    });

                    if (game.Result == 1) wins++;
                    if (game.Result == -1) losses++;
                }
            }

            model.Wins = wins;
            model.Losses = losses;

            return model;
        }

        public async Task<bool> Handle(UpdatePlayerQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;

            var player = _db.GetPlayer(request.Player.Id);

            if (player == null) return false;

            player.Name = request.Player.Name;
            player.CountryId = request.Player.CountryId ?? 255;
            player.IsActive = request.Player.IsActive;

            return _db.UpdatePlayer(player);
        }

        public async Task<bool> Handle(RegisterPlayerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _db.GetUserByExternalId(request.Player.ExternalId.ToString());
                if (user == null)
                {
                    var role = _db.GetRole(request.Player.RoleId);
                    var newUser = new User
                    {
                        ExternalId = request.Player.ExternalId.ToString(),
                        Username = request.Player.Username,
                        Avatar = request.Player.Avatar,
                        Discriminator = request.Player.Discriminator,
                        Email = request.Player.Email ?? string.Empty,
                        Token = request.Player.Token,
                        Role = role
                    };
                    if (!_db.AddUser(newUser))
                    {
                        _logger.LogError($"Failed adding user:{newUser}",
                            DateTimeOffset.UtcNow);
                        return false;
                    }

                    var newPlayer = new Core.PlayerModel()
                    {
                        Name = request.Player.GlobalName,
                        UserId = newUser.Id.ToString(),
                        Token = CommonHelper.GenerateToken(),
                        Games = 0,
                        Wins = 0,
                        IsActive = true
                    };

                    if (!await AddPlayer(newPlayer))
                    {
                        _logger.LogError($"Failed adding player:{JsonSerializer.Serialize(newPlayer)}",
                            DateTimeOffset.UtcNow);
                        return false;
                    }

                    _db.AddPlayer(new Player
                    {
                        Name = request.Player.GlobalName,
                        UserId = newUser.Id,
                        User = newUser,
                        Token = CommonHelper.GenerateToken()
                    });

                    return true;
                }

                _logger.LogInformation($"User exists.", DateTimeOffset.UtcNow);

                user.Username = request.Player.Username;
                user.Avatar = request.Player.Avatar;
                user.Discriminator = request.Player.Discriminator;
                user.Email = request.Player.Email;

                if (user.Player == null)
                {
                    var newPlayer = new Core.PlayerModel
                    {
                        Name = request.Player.GlobalName,
                        UserId = user.ToString(),
                        Token = CommonHelper.GenerateToken(),
                        Games = 0,
                        Wins = 0,
                        IsActive = true
                    };
                    if (!await AddPlayer(newPlayer))
                    {
                        _logger.LogError($"Failed adding player:{JsonSerializer.Serialize(newPlayer)}",
                            DateTimeOffset.UtcNow);
                        return false;
                    }

                    var newPlayerSql = new Player
                    {
                        Name = request.Player.GlobalName,
                        UserId = user.Id,
                        User = user,
                        Token = CommonHelper.GenerateToken()
                    };

                    _db.AddPlayer(newPlayerSql);

                    user.Player = newPlayerSql;
                }
                else
                {
                    user.Player.Name = request.Player.GlobalName;
                    if (string.IsNullOrEmpty(user.Player.Token))
                    {
                        user.Player.Token = string.IsNullOrEmpty(request.Player.Token) ?
                            CommonHelper.GenerateToken() :
                            request.Player.Token;
                    }
                }

                if (!_db.UpdateUser(user))
                {
                    _logger.LogError($"Failed updating user:{JsonSerializer.Serialize(user)}",
                        DateTimeOffset.UtcNow);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\n{ex.StackTrace ?? string.Empty}", DateTimeOffset.UtcNow);
            }
            return false;
        }

        private async Task<bool> AddPlayer(Core.PlayerModel player)
        {
            return await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.PlayerModel, bool>("api/player", player);
        }
    }
}
