using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
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

            var output = _mapper.Map<GetCardDto>(card);

            return Ok(output);
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

        [HttpGet("Board/{boardId}")]
        public async Task<ActionResult<GetCardsDto>> GetBoardCards(Guid boardId)
        {
            var request = new GetBoardCardsQuery
            {
                BoardId = boardId
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
    }
}
