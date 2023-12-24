using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        protected readonly ILogger<CountryController>? _logger;
        protected readonly IMediator? _mediator;

        public CountryController(ILogger<CountryController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("List")]
        public Task<List<CountryModel>> List()
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} get's countries", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetCountriesQuery(locale));
        }
    }
}
