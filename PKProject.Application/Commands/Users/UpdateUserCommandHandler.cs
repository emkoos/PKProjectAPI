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
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetLoggedInUser(request.Email);

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
                Photo = user.Photo
            };

            var result = await _userRepository.UpdateUser(model);

            return result;
        }
    }
}
