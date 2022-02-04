using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Teams
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _repository;

        public DeleteTeamCommandHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.TeamExist(request.Id))
            {
                throw new NotFoundException("Not Found Team");
            }

            var result = await _repository.DeleteTeam(request.Id);

            return result;
        }
    }
}
