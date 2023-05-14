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
        public string Locale { get; }

        public GetTournamentDetailQuery(long tournamentId, string locale)
        {
            TournamentId = tournamentId;
            Locale = locale;
        }
    }

    public class GetTournamentParticipantsQuery : IRequest<IEnumerable<PlayerModel>>
    {
        public long TournamentId { get; }

        public GetTournamentParticipantsQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class GetTournamentListQuery : IRequest<IEnumerable<TournamentModel>>
    {
        public string Locale { get; }

        public GetTournamentListQuery(string locale)
        {
            Locale = locale;
        }
    }

    public class JoinToTournamentQuery : IRequest<bool>
    {
        public int TournamentId { get; }
        public string ExternalId { get; }

        public JoinToTournamentQuery(int tournamentId, string externalId)
        {
            ExternalId = externalId;
            TournamentId = tournamentId;
        }
    }

    public class LeaveTournamentQuery : IRequest<bool>
    {
        public int TournamentId { get; }
        public string ExternalId { get; }

        public LeaveTournamentQuery(int tournamentId, string externalId)
        {
            ExternalId = externalId;
            TournamentId = tournamentId;
        }
    }

    public class GetParticipationStatusQuery : IRequest<bool>
    {
        public int TournamentId { get; }
        public string ExternalId { get; }

        public GetParticipationStatusQuery(int tournamentId, string externalId)
        {
            TournamentId = tournamentId;
            ExternalId = externalId;
        }
    }

    public class TournamentHandler :
        IRequestHandler<GetTournamentDetailQuery, TournamentModel>,
        IRequestHandler<GetTournamentParticipantsQuery, IEnumerable<PlayerModel>>,
        IRequestHandler<GetTournamentListQuery, IEnumerable<TournamentModel>>,
        IRequestHandler<JoinToTournamentQuery, bool>,
        IRequestHandler<LeaveTournamentQuery, bool>,
        IRequestHandler<GetParticipationStatusQuery, bool>
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
            if (tournamentDto == null)
                throw new Exception("Can't retrieve tournament");

            var tournament = _mapper.Map<TournamentModel>(tournamentDto);

            var language = _db.GetLanguage(request.Locale);
            if (language != null)
            {
                var translation = tournamentDto.Translations.FirstOrDefault(t => t.LanguageId == language.Id);
                if (translation != null)
                {
                    tournament.Name = translation.Name;
                    tournament.Description = translation.Description;
                }
            }
            return Task.FromResult(tournament);
        }

        public Task<IEnumerable<PlayerModel>> Handle(GetTournamentParticipantsQuery request, CancellationToken cancellationToken)
        {
            var tournamentDto = _db.GetTournament(request.TournamentId);
            if (tournamentDto == null)
                throw new Exception("Can't retrieve tournament");

            var participants = _mapper.Map<IEnumerable<PlayerModel>>(tournamentDto.Participant);

            return Task.FromResult(participants);
        }

        public Task<IEnumerable<TournamentModel>> Handle(GetTournamentListQuery request, CancellationToken cancellationToken)
        {
            var tournamentsDto = _db.GetTournaments();
            if (tournamentsDto == null)
                throw new Exception("Can't retrieve tournament");

            var tournaments = _mapper.Map<IEnumerable<TournamentModel>>(tournamentsDto);

            var language = _db.GetLanguage(request.Locale);
            if (language != null)
            {
                foreach (var tournament in tournaments)
                {
                    var tournamentDto = tournamentsDto.FirstOrDefault(t => t.Id == tournament.Id);
                    if (tournamentDto != null)
                    {
                        var translation = tournamentDto.Translations.FirstOrDefault(t => t.LanguageId == language.Id);
                        if (translation != null)
                        {
                            tournament.Name = translation.Name;
                            tournament.Description = translation.Description;
                        }
                    }
                }
            }
            return Task.FromResult(tournaments);
        }

        public Task<bool> Handle(JoinToTournamentQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            if (user != null)
            {
                if (_db.AddTournamentParticipant(request.TournamentId, user.Player))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> Handle(LeaveTournamentQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            if (user != null)
            {
                if (_db.RemoveTournamentParticipant(request.TournamentId, user.Player))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> Handle(GetParticipationStatusQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            if (user != null)
            {
                var tournament = _db.GetTournament(request.TournamentId);
                if (tournament != null && tournament.Participant.Any(p => p.UserId == user.Id))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
