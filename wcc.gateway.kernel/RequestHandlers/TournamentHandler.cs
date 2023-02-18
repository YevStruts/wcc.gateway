using MediatR;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetTournamentDetailQuery : IRequest<TournamentModel>
    {
        public long TournamentId { get; }

        public GetTournamentDetailQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class GetTournamentListQuery : IRequest<IEnumerable<TournamentModel>>
    {
    }

    public class TournamentHandler :
        IRequestHandler<GetTournamentDetailQuery, TournamentModel>,
        IRequestHandler<GetTournamentListQuery, IEnumerable<TournamentModel>>
    {
        public Task<TournamentModel> Handle(GetTournamentDetailQuery request, CancellationToken cancellationToken)
        {
            var tournament = FakeDataHelper.GetTournaments().First(t => t.Id == request.TournamentId);
            return Task.FromResult(tournament);
        }

        public Task<IEnumerable<TournamentModel>> Handle(GetTournamentListQuery request, CancellationToken cancellationToken)
        {
            var tournaments = FakeDataHelper.GetTournaments();
            return Task.FromResult(tournaments);
        }
    }
}
