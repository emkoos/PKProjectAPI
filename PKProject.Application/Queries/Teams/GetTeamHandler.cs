using MediatR;
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
    public class GetTeamHandler : IRequestHandler<GetTeamQuery, Team>
    {
        private readonly ITeamRepository _repository;

        public GetTeamHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<Team> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var team = await _repository.GetTeamById(request.Id);

            return team;
        }
    }
}
