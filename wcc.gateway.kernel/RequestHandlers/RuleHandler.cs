using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetRuleDetailQuery : IRequest<RuleModel>
    {
        public long Id { get; }

        public GetRuleDetailQuery(long id)
        {
            Id = id;
        }
    }

    public class RuleHandler : IRequestHandler<GetRuleDetailQuery, RuleModel>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public RuleHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<RuleModel> Handle(GetRuleDetailQuery request, CancellationToken cancellationToken)
        {
            //var ruleDto = _db.GetRule(request.Id);
            var ruleDto = FakeDataHelper.GetRules().FirstOrDefault(r => r.Id == request.Id);
            var rule = _mapper.Map<RuleModel>(ruleDto);
            return Task.FromResult(rule);
        }
    }
}
