using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Queries.BoardTypes;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardTypesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BoardTypesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetBoardTypesDto>> GetBoardTypes()
        {
            var boardTypes = await _mediator.Send(new GetAllBoardTypesQuery());

            if (!boardTypes.Any())
            {
                return NoContent();
            }

            var output = new GetBoardTypesDto()
            {
                BoardTypes = _mapper.Map<List<GetBoardTypeDto>>(boardTypes)
            };

            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBoardTypeDto>> GetBoardType(Guid id)
        {
            var request = new GetBoardTypeQuery
            {
                Id = id
            };

            var boardType = await _mediator.Send(request);

            if (boardType is null)
            {
                return NoContent();
            }

            var output = _mapper.Map<GetBoardTypeDto>(boardType);

            return Ok(output);
        }
    }
}
