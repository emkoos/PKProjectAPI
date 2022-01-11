using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Teams
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(IMediator mediator, ITeamRepository teamRepository)
        {
            _mediator = mediator;
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = "Bez nazwy";
            }

            var model = new Team
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            var result = await _teamRepository.CreateTeam(model);

            return result;
        }
    }
}
