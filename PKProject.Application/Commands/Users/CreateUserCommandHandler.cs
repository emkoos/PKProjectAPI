﻿using MediatR;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        public readonly IMediator _mediator;
        public readonly IUserRepository _repository;

        public CreateUserCommandHandler(IMediator mediator, IUserRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (await _repository.UserExist(request.Email))
            {
                throw new Exception("User with that email already exists.");
            }

            var model = new User
            {
                Email = request.Email,
                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Photo = request.Photo
            };

            var result = await _repository.CreateUser(model);

            return result;
        }
    }
}