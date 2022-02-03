using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Commands.Comments;
using PKProject.Application.Queries.Comments;
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

            var commentDto = new List<GetCommentDto>();

            foreach (var comment in comments)
            {
                commentDto.Add(new GetCommentDto
                {
                    Id = comment.Id,
                    UserEmail = comment.UserEmail,
                    CardId = comment.CardId,
                    Content = comment.Content,
                    Date = comment.Date.ToString()
                });
            }

            var output = new GetCommentsDto()
            {
                Comments = commentDto
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] CreateNewCommentCommand model)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.Email);

            if (loggedInUser is null)
            {
                return Unauthorized();
            }

            var newModel = new CreateCommentCommand
            {
                UserEmail = loggedInUser,
                CardId = model.CardId,
                Content = model.Content
            };

            await _mediator.Send(newModel);
            return Created($"/comments/comment", null);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditComment([FromBody] UpdateCommentCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var model = new DeleteCommentCommand
            {
                Id = id
            };

            await _mediator.Send(model);
            return Ok();
        }
    }
}
