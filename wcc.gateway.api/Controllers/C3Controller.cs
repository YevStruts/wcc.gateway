using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using wcc.gateway.api.Helpers;
using wcc.gateway.Identity;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.C3;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class C3Controller : ControllerBase
    {
        protected readonly ILogger<CountryController>? _logger;
        protected readonly IMediator? _mediator;

        public C3Controller(ILogger<CountryController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("Login")]
        public async Task<C3PlayerModel> Login(LoginModel model)
        {
            _logger.LogInformation($"User:{User.GetUserId()} logins from c3", DateTimeOffset.UtcNow);
            return await _mediator.Send(new LoginQuery(model.Token));
        }

        [HttpGet, Route("Rating")]
        public async Task<C3RatingModel> GetRating()
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} get's rating Id:{1}", DateTimeOffset.UtcNow);
            
            var rating = await _mediator.Send(new GetRatingQuery(1, locale));

            var model = new C3RatingModel() { result = true }; 
            rating.ForEach(p => model.players.Add(new C3RatingItemModel
            {
                id = p.Id,
                name = p.Name,
                score = p.TotalPoints
            }));

            return model;
        }
    }
}
