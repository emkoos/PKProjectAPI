using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Application.Commands.Cards;
using PKProject.Application.Commands.Users;
using PKProject.Application.Queries.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CardsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDto>> GetCard(Guid id)
        {
            var request = new GetCardQuery
            {
                Id = id
            };

            var card = await _mediator.Send(request);

            if (card is null)
            {
                return NoContent();
            }

            
            return Ok(card);
        }

        [HttpGet("User/{email}")]
        public async Task<ActionResult<GetCardsDto>> GetUserCards(string email)
        {
            var request = new GetUserCardsQuery
            {
                Email = email
            };

            var cards = await _mediator.Send(request);

            if (!cards.Any())
            {
                return NoContent();
            }

            var output = new GetCardsDto()
            {
                Cards = _mapper.Map<List<GetCardDto>>(cards)
            };

            return Ok(output);
        }

        [HttpGet("Column/{columnId}")]
        public async Task<ActionResult<GetCardsDto>> GetColumnCards(Guid columnId)
        {
            var request = new GetColumnCardsQuery
            {
                ColumnId = columnId
            };

            var cards = await _mediator.Send(request);

            if (!cards.Any())
            {
                return NoContent();
            }

            var cardDto = new List<GetCardDto>();

            foreach (var card in cards)
            {
                cardDto.Add(new GetCardDto
                {
                    Id = card.Id,
                    Title = card.Title,
                    Description = card.Description,
                    UserEmail = card.UserEmail,
                    ColumnId = card.ColumnId,
                    StatusId = card.StatusId,
                    CreatedDate = card.CreatedDate,
                    UpdatedStatusDoneDate = card.UpdatedStatusDoneDate,
                    DeadlineDate = card.DeadlineDate.ToString(),
                    Priority = card.Priority,
                    Estimate = card.Estimate,
                    Attachement = card.Attachement
                });
            }

            var output = new GetCardsDto()
            {
                Cards = cardDto
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardCommand model)
        {
            await _mediator.Send(model);
            return Created($"/cards/card", null);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCard([FromBody] UpdateCardCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var model = new DeleteCardCommand
            {
                Id = id
            };

            await _mediator.Send(model);
            return Ok();
        }

        [HttpPut("edit-user")]
        public async Task<IActionResult> EditCardUser([FromBody] UpdateCardUserCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }
    }
}
