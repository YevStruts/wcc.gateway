using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetUserWhoAmIQuery : IRequest<WhoAmIModel>
    {
        public string ExternalId { get; }

        public GetUserWhoAmIQuery(string externalId)
        {
            ExternalId = externalId;
        }
    }

    public class UserHandler : IRequestHandler<GetUserWhoAmIQuery, WhoAmIModel>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public UserHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<WhoAmIModel> Handle(GetUserWhoAmIQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            var model = _mapper.Map<WhoAmIModel>(user);
            return Task.FromResult(model);
        }
    }
}
