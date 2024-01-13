using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetSettingsQuery : IRequest<SettingsModel>
    {
        public string ExternalId { get; }
        public GetSettingsQuery(string externalId)
        {
            ExternalId = externalId;
        }
    }

    public class SaveSettingsQuery : IRequest<bool>
    {
        public string ExternalId { get; }
        public string Nickname { get; }

        public SaveSettingsQuery(string externalId, SettingsModel model)
        {
            ExternalId = externalId;
            Nickname = model.Nickname;
        }
    }

    public class RequestTokenQuery : IRequest<bool>
    {
        public string Username { get; }
        public RequestTokenQuery(string username)
        {
            Username = username;
        }
    }

    public class SettingsHandler :
        IRequestHandler<GetSettingsQuery, SettingsModel>,
        IRequestHandler<SaveSettingsQuery, bool>,
        IRequestHandler<RequestTokenQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public SettingsHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<SettingsModel> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            if (user == null)
                throw new Exception("Can't retrieve user");

            var model = new SettingsModel
            {
                Nickname = user.Player?.Name ?? string.Empty,
                Token = user.Player?.Token ?? string.Empty,
            };

            return Task.FromResult(model);
        }

        public Task<bool> Handle(SaveSettingsQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalId);
            if (user == null)
                throw new Exception("Can't retrieve user");

            user.Player.Name = request.Nickname;
            return Task.FromResult(_db.UpdatePlayer(user.Player));
        }

        public async Task<bool> Handle(RequestTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByUsername(request.Username);
            if (user == null || !string.IsNullOrEmpty(user.Player.Token))
                return false;

            user.Player.Token = CommonHelper.GenerateToken();

            return _db.UpdatePlayer(user.Player);
        }
    }
}
