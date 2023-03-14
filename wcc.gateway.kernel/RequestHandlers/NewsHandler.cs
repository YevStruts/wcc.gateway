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

        public string Locale { get; }

        public GetNewsDetailQuery(long id, string locale)
        {
            Id = id;
            Locale = locale;
        }
    }

    public class GetNewsListQuery : IRequest<IEnumerable<NewsModel>>
    {
        public string Locale { get; }
        public GetNewsListQuery(string locale)
        {
            Locale = locale;
        }
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
            var newsDto = _db.GetNews(request.Id);
            if (newsDto == null)
                throw new Exception("News does not exist.");

            var news = _mapper.Map<NewsModel>(newsDto);

            var language = _db.GetLanguage(request.Locale);
            if (language != null)
            {
                var translation = newsDto.Translations.FirstOrDefault(t => t.LanguageId == language.Id);
                if (translation != null)
                {
                    news.Name = translation.Name;
                    news.Description = translation.Description;
                }
            }

            return Task.FromResult(news);
        }

        public Task<IEnumerable<NewsModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var newsDto = _db.GetNewsList();
            if (newsDto == null)
                throw new Exception("Can't retrieve news");

            var news = _mapper.Map<IEnumerable<NewsModel>>(newsDto);
            
            var language = _db.GetLanguage(request.Locale);
            if (language != null)
            {
                foreach (var newsItem in news)
                {
                    var newsItemDto = newsDto.FirstOrDefault(t => t.Id == newsItem.Id);
                    if (newsItemDto != null)
                    {
                        var translation = newsItemDto.Translations.FirstOrDefault(t => t.LanguageId == language.Id);
                        if (translation != null )
                        {
                            newsItem.Name = translation.Name;
                            newsItem.Description = translation.Description;
                        }
                    }
                }
            }

            return Task.FromResult(news);
        }
    }
}
