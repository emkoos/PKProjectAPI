using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Commands.Boards;
using PKProject.Application.Queries.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BoardsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBoardDto>> GetBoard(Guid id)
        {
            var request = new GetBoardQuery
            {
                Id = id
            };

            var board = await _mediator.Send(request);

            if (board is null)
            {
                return NoContent();
            }

            var output = _mapper.Map<GetBoardDto>(board);

            return Ok(output);
        }

        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult<GetBoardsDto>> GetTeamBoards(Guid teamId)
        {
            var request = new GetTeamBoardsQuery
            {
                TeamId = teamId
            };

            var boards = await _mediator.Send(request);

            if (!boards.Any())
            {
                return NoContent();
            }

            var output = new GetBoardsDto()
            {
                Boards = _mapper.Map<List<GetBoardDto>>(boards)
            };

            return Ok(output);
        }

        [HttpGet("Teams/my")]
        public async Task<ActionResult<GetBoardsDto>> GetMyAllTeamsBoards()
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.Email);

            var boards = await _mediator.Send(new GetMyAllBoardsQuery { Email = loggedInUser });

            if (!boards.Any())
            {
                return NoContent();
            }

            var output = new GetBoardsDto()
            {
                Boards = _mapper.Map<List<GetBoardDto>>(boards)
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand model)
        {
            var result = await _mediator.Send(model);
            return Created($"/boards/board", result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditBoard([FromBody] UpdateBoardCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            var model = new DeleteBoardCommand
            {
                Id = id
            };

            await _mediator.Send(model);
            return Ok();
        }
    }
}
