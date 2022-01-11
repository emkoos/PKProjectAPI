using MediatR;
using Microsoft.AspNetCore.Identity;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Teams
{
    public class GetLoggedInUserTeamsHandler : IRequestHandler<GetLoggedInUserTeamsQuery, IEnumerable<Team>>
    {
        private readonly ITeamRepository _repository;

        public GetLoggedInUserTeamsHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Team>> Handle(GetLoggedInUserTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _repository.GetLoggedInUserTeams(request.Email);

            return teams;
        }
    }
}
