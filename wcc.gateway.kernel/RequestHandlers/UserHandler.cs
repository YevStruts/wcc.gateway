using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.User;

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

    public class GetUserByUsernameQuery : IRequest<UserModel>
    {
        public string Username { get; }
        public GetUserByUsernameQuery(string username)
        {
            this.Username = username;
        }
    }

    public class UserHandler : IRequestHandler<GetUserWhoAmIQuery, WhoAmIModel>,
        IRequestHandler<GetUserByUsernameQuery, UserModel>
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

        public async Task<UserModel> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByUsername(request.Username);
            return _mapper.Map<UserModel>(user);
        }
    }
}
