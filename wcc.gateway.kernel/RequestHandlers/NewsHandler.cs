using MediatR;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class News : IRequest<NewsModel>
    {
        public long? Id { get; set; }
    }

    public class NewsHandler : IRequestHandler<News, NewsModel>
    {
        public Task<NewsModel> Handle(News request, CancellationToken cancellationToken)
        {
            var news = FakeDataHelper.GetNews().First(n => n.Id == request.Id);
            return Task.FromResult(news);
        }
    }
}
