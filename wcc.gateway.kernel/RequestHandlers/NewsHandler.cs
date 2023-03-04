using MediatR;
using wcc.gateway.data;
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
        private readonly IDataRepository _db;

        public NewsHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<NewsModel> Handle(GetNewsDetailQuery request, CancellationToken cancellationToken)
        {
            var news = _db.GetNews(request.Id);

            var model = new NewsModel();
            model.FromDto(news);

            return Task.FromResult(model);
        }

        public Task<IEnumerable<NewsModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var news = _db.GetNewsList().ToList();
            
            var model = new List<NewsModel>();
            news.ForEach(n =>
            {
                var item = new NewsModel();
                item.FromDto(n);
                model.Add(item);
            });

            return Task.FromResult<IEnumerable<NewsModel>>(model);
        }
    }
}
