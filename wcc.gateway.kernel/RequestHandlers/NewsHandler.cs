using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
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
        private readonly IMapper _mapper = MapperHelper.Instance;

        public NewsHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<NewsModel> Handle(GetNewsDetailQuery request, CancellationToken cancellationToken)
        {
            var news = _db.GetNews(request.Id);

            var model = _mapper.Map<News, NewsModel>(news);

            return Task.FromResult(model);
        }

        public Task<IEnumerable<NewsModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var news = _db.GetNewsList().ToList();
            
            var model = _mapper.Map<List<News>, List<NewsModel>>(news);

            return Task.FromResult<IEnumerable<NewsModel>>(model);
        }
    }
}
