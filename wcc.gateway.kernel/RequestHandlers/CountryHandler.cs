using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetCountriesQuery : IRequest<List<CountryModel>>
    {
        public string Locale { get; }
        public GetCountriesQuery(string locale)
        {
            Locale = locale;
        }
    }

    public class CountryHandler :
        IRequestHandler<GetCountriesQuery, List<CountryModel>>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public CountryHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<List<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countriesDto = _db.GetCountries().OrderBy(n => n.Name).ToList();
            if (countriesDto == null)
                throw new Exception("Can't retrieve news");

            return _mapper.Map<List<CountryModel>>(countriesDto);
        }
    }
}
