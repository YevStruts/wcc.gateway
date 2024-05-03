using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Standing;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Core = wcc.gateway.kernel.Models.Core;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetRoundRobinQuery : IRequest<List<RRGameModel>>
    {
        public int TournamentId { get; }

        public GetRoundRobinQuery(int tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class StandingHandler : IRequestHandler<GetRoundRobinQuery, List<RRGameModel>>
    {
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public StandingHandler(Microservices.Config mcsvcConfig)
        {
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<List<RRGameModel>> Handle(GetRoundRobinQuery request, CancellationToken cancellationToken)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("tournamentId", $"tournaments/{request.TournamentId}-A");
            parameters.Add("page", "1");
            parameters.Add("count", $"{int.MaxValue}");

            var games = await new ApiCaller(_mcsvcConfig.CoreUrl)
                .GetAsync<List<Core.GameModel>>($"api/Game?{parameters}");

            var players = await new ApiCaller(_mcsvcConfig.CoreUrl)
                .GetAsync<List<Core.PlayerModel>>($"api/player");

            var teams = await new ApiCaller(_mcsvcConfig.CoreUrl)
                .GetAsync<List<Core.TeamModel>>($"api/team");

            List<RRGameModel> model = new List<RRGameModel>();
            foreach (var game in games)
            {
                string sideA = game.GameType == Infrastructure.GameType.Individual ?
                    string.Join("/", game.SideA.Select(s => players.First(p => p.Id == s).Name)) :
                    string.Join("/", game.SideA.Select(s => teams.First(p => p.Id == s).Name));

                string sideB = game.GameType == Infrastructure.GameType.Individual ?
                    string.Join("/", game.SideB.Select(s => players.First(p => p.Id == s).Name)) :
                    string.Join("/", game.SideB.Select(s => teams.First(p => p.Id == s).Name));

                model.Add(new RRGameModel
                {
                    SideA = sideA,
                    SideB = sideB,
                    Result = $"{game.ScoreA}-{game.ScoreB}"
                });
            }

            return model;
        }
    }
}
