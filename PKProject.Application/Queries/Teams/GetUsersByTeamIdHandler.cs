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
    public class GetUsersByTeamIdHandler : IRequestHandler<GetUsersByTeamIdQuery, IEnumerable<User>>
    {
        private readonly ITeamRepository _repository;

        public GetUsersByTeamIdHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetUsersByTeamId(request.TeamId);

            return users;
        }
    }
}
