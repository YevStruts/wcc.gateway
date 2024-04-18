using AutoMapper;
using MediatR;
using System.Linq;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.integrations.Discord.Helpers;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Core = wcc.gateway.kernel.Models.Core;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetTournamentsQuery : IRequest<IList<TournamentModel>>
    {

    }

    public class GetTournamentDetailQuery : IRequest<TournamentModelOld>
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

    public class GetTournamentListQuery : IRequest<IEnumerable<TournamentModelOld>>
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

    public class GetTournamentSwitzTableQuery : IRequest<List<SwitzTableItemModel>>
    {
        public long TournamentId { get; }

        public GetTournamentSwitzTableQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class TournamentHandler :
        IRequestHandler<GetTournamentsQuery, IList<TournamentModel>>,
        IRequestHandler<GetTournamentDetailQuery, TournamentModelOld>,
        IRequestHandler<GetTournamentParticipantsQuery, IEnumerable<PlayerModel>>,
        IRequestHandler<GetTournamentListQuery, IEnumerable<TournamentModelOld>>,
        IRequestHandler<JoinToTournamentQuery, bool>,
        IRequestHandler<LeaveTournamentQuery, bool>,
        IRequestHandler<GetParticipationStatusQuery, bool>,
        IRequestHandler<GetTournamentSwitzTableQuery, List<SwitzTableItemModel>>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public TournamentHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<IList<TournamentModel>> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.TournamentModel>>($"api/tournament");
            return _mapper.Map<List<TournamentModel>>(tournaments);
        }

        public Task<TournamentModelOld> Handle(GetTournamentDetailQuery request, CancellationToken cancellationToken)
        {
            var tournamentDto = _db.GetTournament(request.TournamentId);
            if (tournamentDto == null)
                throw new Exception("Can't retrieve tournament");

            var tournament = _mapper.Map<TournamentModelOld>(tournamentDto);

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

            bool isTeamsCompetition = tournamentDto.Games.Any(g => g.GameType == GameType.Teams);

            var participants = isTeamsCompetition ?
                _mapper.Map<IEnumerable<PlayerModel>>(tournamentDto.Teams) :
                _mapper.Map<IEnumerable<PlayerModel>>(tournamentDto.Participant);

            return Task.FromResult(participants);
        }

        public Task<IEnumerable<TournamentModelOld>> Handle(GetTournamentListQuery request, CancellationToken cancellationToken)
        {
            var tournamentsDto = _db.GetTournaments();
            if (tournamentsDto == null)
                throw new Exception("Can't retrieve tournament");

            var tournaments = _mapper.Map<IEnumerable<TournamentModelOld>>(tournamentsDto);

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

        public async Task<List<SwitzTableItemModel>> Handle(GetTournamentSwitzTableQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers().Where(p => p.Tournament.Select(t => t.Id).Contains(request.TournamentId));
            var gamesDto = _db.GetGames().Where(g => g.TournamentId == request.TournamentId && g.Id < 245).ToList();
            
            var records = new List<SwitzTableItemModel>();
            var games = gamesDto.Where(g => g.HScore + g.VScore > 0).ToList();

            var playerRatingData = await new ApiCaller(_mcsvcConfig.RatingUrl).GetAsync<List<PlayerData>>("api/rating");
            var rating = playerRatingData.OrderByDescending(r => r.Points).ToList();

            foreach (var player in players)
            {
                var hgames = games.Where(g => g.HUserId == player.UserId).ToList();
                var vgames = games.Where(g => g.VUserId == player.UserId).ToList();

                // games
                var hgameswin = hgames.Where(g => g.HUserId == player.UserId && g.HScore > g.VScore).ToList();
                var vgameswin = vgames.Where(g => g.VUserId == player.UserId && g.HScore < g.VScore).ToList();

                var hgamesloss = hgames.Where(g => g.HUserId == player.UserId && g.HScore < g.VScore).ToList();
                var vgamesloss = vgames.Where(g => g.VUserId == player.UserId && g.HScore > g.VScore).ToList();

                var hwins = hgameswin.Count();
                var vwins = vgameswin.Count();

                var hloss = hgamesloss.Count();
                var vloss = vgamesloss.Count();

                // scores
                var scoreswon = hgameswin.Select(g => g.HScore).Sum() + vgameswin.Select(g => g.VScore).Sum() +
                                hgamesloss.Select(g => g.HScore).Sum() + vgamesloss.Select(g => g.VScore).Sum();
                int scoresloss = hgameswin.Select(g => g.VScore).Sum() + vgameswin.Select(g => g.HScore).Sum() +
                                 hgamesloss.Select(g => g.VScore).Sum() + vgamesloss.Select(g => g.HScore).Sum();

                var oppUserIdWon = new List<long>();
                oppUserIdWon.AddRange(hgameswin.Select(p => p.VUserId).ToList());
                oppUserIdWon.AddRange(vgameswin.Select(p => p.HUserId).ToList());

                var oppUserIdLoss = new List<long>();
                oppUserIdLoss.AddRange(hgamesloss.Select(p => p.VUserId).ToList());
                oppUserIdLoss.AddRange(vgamesloss.Select(p => p.HUserId).ToList());

                float averageRatingOppWon = getAverageRatingOpponent(rating, players, oppUserIdWon);
                float averageRatingOppLoss = getAverageRatingOpponent(rating, players, oppUserIdLoss);

                var record = new SwitzTableItemModel()
                {
                    Name = player.Name,
                    Avatar = DiscordHelper.GetAvatarUrl(player.User.ExternalId, player.User.Avatar),
                    GamesCount = hgames.Count + vgames.Count,
                    ScoresWon = scoreswon,
                    ScoresLoss = scoresloss,
                    AverageRatingOppWon = averageRatingOppWon,
                    AverageRatingOppLoss = averageRatingOppLoss,
                    Scores = hwins + vwins
                };

                records.Add(record);
            }

            return records;
        }

        private float getAverageRatingOpponent(List<PlayerData> rating, IEnumerable<Player> players, List<long> oppUserIds)
        {
            if (oppUserIds.Count == 0) return rating.Count / 2;

            var oppPositions = new List<int>();
            foreach (var oppUserId in oppUserIds)
            {
                var player = players.First(p => p.UserId == oppUserId);

                var ratingItem = rating.FirstOrDefault(r => r.PlayerId == player.Id);
                if (ratingItem != null)
                {
                    var position = rating.IndexOf(ratingItem);
                    oppPositions.Add(position);
                }
                oppPositions.Add(rating.Count / 2);
            }
            return oppPositions.Sum() / oppPositions.Count();
        }
    }
}
