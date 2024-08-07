﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Schedule;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        protected readonly ILogger<ScheduleController>? _logger;
        protected readonly IMediator? _mediator;

        public ScheduleController(ILogger<ScheduleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IList<ScheduleModel>> Get(string? tournamentId = null, long page = 1, long count = 20)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's game Id:{1}", DateTimeOffset.UtcNow);
            return await _mediator.Send(new GetScheduleQuery(tournamentId, page, count));
        }

        [HttpGet, Route("Count")]
        public async Task<int> Count(string? tournamentId = null)
        {
            return await _mediator.Send(new GetScheduleCountQuery(tournamentId));
        }
    }
}
