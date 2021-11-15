using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Queries.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StatusesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetStatusesDto>> GetStatuses()
        {
            var statuses = await _mediator.Send(new GetAllStatusesQuery());

            if (!statuses.Any())
            {
                return NoContent();
            }

            var output = new GetStatusesDto()
            {
                Statuses = _mapper.Map<List<GetStatusDto>>(statuses)
            };

            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStatusDto>> GetStatus(Guid id)
        {
            var request = new GetStatusQuery
            {
                Id = id
            };

            var status = await _mediator.Send(request);

            if (status is null)
            {
                return NoContent();
            }

            var output = _mapper.Map<GetStatusDto>(status);

            return Ok(output);
        }
    }
}
