using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool?>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<bool?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExist(request.Email))
            {
                throw new Exception("Not Found User");
            }

            var model = new User
            {
                Email = request.Email,
                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Photo = request.Photo
            };

            var result = await _userRepository.UpdateUser(model);

            return result;
        }
    }
}
