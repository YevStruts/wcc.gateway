﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IMediator _mediator;

        public PlayerController(ILogger<PlayerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<PlayerModel> Get(int id)
        {
            return _mediator.Send(new Player { Id = id });
        }

        [HttpGet, Route("List")]
        public IEnumerable<PlayerModel> List()
        {
            //return FakeDataHelper.GetPlayers();

            throw new NotImplementedException();
        }
    }
}
