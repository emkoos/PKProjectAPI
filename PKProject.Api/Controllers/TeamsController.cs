using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PKProject.Api.DTO;
using PKProject.Api.DTO.Users;
using PKProject.Application.Commands.Teams;
using PKProject.Application.Queries.Statuses;
using PKProject.Application.Queries.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public TeamsController(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("teams/my")]
        public async Task<ActionResult<GetTeamsDto>> GetLoggedInUserTeams()
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.Email);
            var teams = await _mediator.Send(new GetLoggedInUserTeamsQuery() { Email = loggedInUser });
            
            if (!teams.Any())
            {
                return NoContent();
            }

            var output = new GetTeamsDto()
            {
                Teams = _mapper.Map<List<GetTeamDto>>(teams)
            };

            return Ok(output);
        }

        [HttpGet("teams/users/{teamId}")]
        public async Task<ActionResult<GetUsersDto>> GetUsersByTeamId(Guid teamId)
        {
            var users = await _mediator.Send(new GetUsersByTeamIdQuery() { TeamId = teamId });

            if (!users.Any())
            {
                return NoContent();
            }

            var output = new GetUsersDto()
            {
                Users = _mapper.Map<List<GetUserDto>>(users)
            };

            return Ok(output);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand model)
        {
            await _mediator.Send(model);
            return Created($"/teams/team", null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var model = new DeleteTeamCommand
            {
                Id = id
            };

            await _mediator.Send(model);
            return Ok();
        }
    }
}
