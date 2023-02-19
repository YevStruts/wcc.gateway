using MediatR;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetNewsDetailQuery : IRequest<NewsModel>
    {
        public long Id { get; }

        public GetNewsDetailQuery(long id)
        {
            Id = id;
        }
    }

    public class GetNewsListQuery : IRequest<IEnumerable<NewsModel>>
    {
    }

    public class NewsHandler :
        IRequestHandler<GetNewsDetailQuery, NewsModel>,
        IRequestHandler<GetNewsListQuery, IEnumerable<NewsModel>>
    {
        public Task<NewsModel> Handle(GetNewsDetailQuery request, CancellationToken cancellationToken)
        {
            var news = FakeDataHelper.GetNews().First(n => n.Id == request.Id);
            return Task.FromResult(news);
        }

        public Task<IEnumerable<NewsModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var news = FakeDataHelper.GetNews();
            return Task.FromResult(news);
        }
    }
}
