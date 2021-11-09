using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Commands.Comments;
using PKProject.Application.Queries.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCommentDto>> GetComment(Guid id)
        {
            var request = new GetCommentQuery
            {
                Id = id
            };

            var comment = await _mediator.Send(request);

            if (comment is null)
            {
                return NoContent();
            }

            var output = _mapper.Map<GetCommentDto>(comment);

            return Ok(output);
        }

        [HttpGet("User/{email}")]
        public async Task<ActionResult<GetCommentsDto>> GetUserComments(string email)
        {
            var request = new GetUserCommentsQuery
            {
                Email = email
            };

            var comments = await _mediator.Send(request);

            if (!comments.Any())
            {
                return NoContent();
            }

            var output = new GetCommentsDto()
            {
                Comments = _mapper.Map<List<GetCommentDto>>(comments)
            };

            return Ok(output);
        }

        [HttpGet("Card/{cardId}")]
        public async Task<ActionResult<GetCommentsDto>> GetCardComments(Guid cardId)
        {
            var request = new GetCardCommentsQuery
            {
                CardId = cardId
            };

            var comments = await _mediator.Send(request);

            if (!comments.Any())
            {
                return NoContent();
            }

            var output = new GetCommentsDto()
            {
                Comments = _mapper.Map<List<GetCommentDto>>(comments)
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand model)
        {
            await _mediator.Send(model);
            return Created($"/comments/comment", null);
        }
    }
}
