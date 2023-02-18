using MediatR;
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
        public Task<RuleModel> Handle(GetRuleDetailQuery request, CancellationToken cancellationToken)
        {
            var rule = FakeDataHelper.GetRules().First(r => r.Id == request.Id);
            return Task.FromResult(rule);
        }
    }
}
