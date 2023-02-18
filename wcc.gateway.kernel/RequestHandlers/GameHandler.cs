using MediatR;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetGameDetailQuery : IRequest<GameModel>
    {
        public long Id { get; }

        public GetGameDetailQuery(long id)
        {
            Id = id;
        }
    }

    public class GetGameListQuery : IRequest<IEnumerable<GameListModel>>
    {
        public long TournamentId { get; }

        public GetGameListQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class GameHandler :
        IRequestHandler<GetGameDetailQuery, GameModel>,
        IRequestHandler<GetGameListQuery, IEnumerable<GameListModel>>
    {
        public Task<GameModel> Handle(GetGameDetailQuery request, CancellationToken cancellationToken)
        {
            var game = FakeDataHelper.GetGames().First(g => g.Id == request.Id) as GameModel;
            return Task.FromResult(game);
        }
        public Task<IEnumerable<GameListModel>> Handle(GetGameListQuery request, CancellationToken cancellationToken)
        {
            var games = FakeDataHelper.GetGames();
            return Task.FromResult(games);
        }
    }
}
