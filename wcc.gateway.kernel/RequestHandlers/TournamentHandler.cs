using AutoMapper;
using MediatR;
using wcc.gateway.data;
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
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public TournamentHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<TournamentModel> Handle(GetTournamentDetailQuery request, CancellationToken cancellationToken)
        {
            var tournamentDto = _db.GetTournament(request.TournamentId);
            var tournament = _mapper.Map<TournamentModel>(tournamentDto);
            return Task.FromResult(tournament);
        }

        public Task<IEnumerable<TournamentModel>> Handle(GetTournamentListQuery request, CancellationToken cancellationToken)
        {
            var tournamentsDto = _db.GetTournaments();
            var tournaments = _mapper.Map<IEnumerable<TournamentModel>>(tournamentsDto);
            return Task.FromResult(tournaments);
        }
    }
}
