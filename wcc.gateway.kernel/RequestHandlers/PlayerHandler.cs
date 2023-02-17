using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class Player : IRequest<PlayerModel>
    {
        public long? Id { get; set; }
    }

    public class PlayerHandler : IRequestHandler<Player, PlayerModel>
    {
        public Task<PlayerModel> Handle(Player request, CancellationToken cancellationToken)
        {
            var player = FakeDataHelper.GetPlayers().First(n => n.Id == request.Id) as PlayerModel;
            return Task.FromResult(player);
        }
    }
}
