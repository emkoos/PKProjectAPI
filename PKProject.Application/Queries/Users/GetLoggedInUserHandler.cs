using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Users
{
    public class GetLoggedInUserHandler : IRequestHandler<GetLoggedInUserQuery, User>
    {
        private readonly IUserRepository _repository;

        public GetLoggedInUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetLoggedInUser(request.UserEmail);

            return user;
        }
    }
}
