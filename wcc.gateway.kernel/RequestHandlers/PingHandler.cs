using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class Ping : IRequest<string>
    {
        public string ResponseMessage { get; set; }
    }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.ResponseMessage);
        }
    }
}
