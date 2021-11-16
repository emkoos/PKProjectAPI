using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Commands.Columns;
using PKProject.Application.Queries.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ColumnsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetColumnDto>> GetColumn(Guid id)
        {
            var request = new GetColumnQuery
            {
                Id = id
            };

            var column = await _mediator.Send(request);

            if (column is null)
            {
                return NoContent();
            }

            var output = _mapper.Map<GetColumnDto>(column);

            return Ok(output);
        }

        [HttpGet("Board/{boardId}")]
        public async Task<ActionResult<GetColumnsDto>> GetBoardColumns(Guid boardId)
        {
            var request = new GetBoardColumnsQuery
            {
                BoardId = boardId
            };

            var columns = await _mediator.Send(request);

            if (!columns.Any())
            {
                return NoContent();
            }

            var output = new GetColumnsDto()
            {
                Columns = _mapper.Map<List<GetColumnDto>>(columns)
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateColumn([FromBody] CreateColumnCommand model)
        {
            await _mediator.Send(model);
            return Created($"/columns/column", null);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditColumn([FromBody] UpdateColumnCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(Guid id)
        {
            var model = new DeleteColumnCommand
            {
                Id = id
            };

            await _mediator.Send(model);
            return Ok();
        }
    }
}
