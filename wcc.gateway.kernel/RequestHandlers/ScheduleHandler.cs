using AutoMapper;
using MediatR;
using System.Collections.Specialized;
using System.Numerics;
using System.Web;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Communication.Core;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Schedule;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Core = wcc.gateway.kernel.Models.Core;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetScheduleQuery : IRequest<IList<ScheduleModel>>
    {
        public string TournamentId { get; }
        public long Page { get; }
        public long Count { get; }

        public GetScheduleQuery(string tournamentId, long page, long count)
        {
            TournamentId = tournamentId;
            Page = page;
            Count = count;
        }
    }

    public class GetScheduleCountQuery : IRequest<int>
    {
        public string TournamentId { get; }

        public GetScheduleCountQuery(string tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class ScheduleHandler :
        IRequestHandler<GetScheduleQuery, IList<ScheduleModel>>,
        IRequestHandler<GetScheduleCountQuery, int>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public ScheduleHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<IList<ScheduleModel>> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrEmpty(request.TournamentId))
            {
                parameters.Add("tournamentId", request.TournamentId);
            }
            parameters.Add("page", request.Page.ToString());
            parameters.Add("count", request.Count.ToString());
                
            var games = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<GameData>>($"api/game?{parameters}");

            var players = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.PlayerModel>>($"api/player");

            var teams = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.TeamModel>>($"api/team");

            var schedule = new List<ScheduleModel>();
            foreach (var game in games)
            {
                string sideA = game.GameType == GameType.Individual ?
                    CreateSideTitle(game.SideA, players.Select(p => Tuple.Create(p.Id, p.Name)).ToList()) :
                    CreateSideTitle(game.SideA, teams.Select(p => Tuple.Create(p.Id, p.Name)).ToList());

                string sideB = game.GameType == GameType.Individual ?
                    CreateSideTitle(game.SideB, players.Select(p => Tuple.Create(p.Id, p.Name)).ToList()) :
                    CreateSideTitle(game.SideB, teams.Select(p => Tuple.Create(p.Id, p.Name)).ToList());

                schedule.Add(new ScheduleModel
                {
                    Id = game.Id,
                    Scheduled = game.Scheduled,
                    Name = "name",
                    SideA = sideA,
                    SideB = sideB,
                    ScoreA = game.ScoreA,
                    ScoreB = game.ScoreB,
                    YouTube = game.Youtube.Where(y => !string.IsNullOrEmpty(y)).ToList()
                });
            }

            return schedule;
        }

        public async Task<int> Handle(GetScheduleCountQuery request, CancellationToken cancellationToken)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrEmpty(request.TournamentId))
            {
                parameters.Add("tournamentId", request.TournamentId);
            }
            return await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<int>($"api/game/count?{parameters}");
        }

        private string CreateSideTitle(List<string>? side, List<Tuple<string? /* Id */, string? /* Name */>> list)
        {
            var participants = list.Where(l => side.Contains(l.Item1)).Select(l => l.Item2).ToList();
            return string.Join("/", participants);
        }
    }
}
