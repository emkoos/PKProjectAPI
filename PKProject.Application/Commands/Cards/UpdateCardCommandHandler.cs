﻿using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Cards
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, bool?>
    {
        public readonly IMediator _mediator;
        public readonly ICardRepository _cardRepository;
        public readonly IUserRepository _userRepository;
        public readonly IBoardRepository _boardRepository;

        public UpdateCardCommandHandler(IMediator mediator, ICardRepository cardRepository, IUserRepository userRepository, IBoardRepository boardRepository)
        {
            _mediator = mediator;
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _boardRepository = boardRepository;
        }

        public async Task<bool?> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            if (!await _cardRepository.CardExist(request.Id))
            {
                throw new Exception("Not Found Card");
            }

            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new Exception("Not Found User");
            }

            if (!await _boardRepository.BoardExist(request.BoardId))
            {
                throw new Exception("Not Found Board");
            }

            if (String.IsNullOrWhiteSpace(request.Description))
            {
                request.Description = "";
            }

            var model = new Card
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                UserEmail = request.UserEmail,
                BoardId = request.BoardId,
                StatusId = request.StatusId,
                DeadlineDate = request.DeadlineDate,
                Priority = request.Priority,
                Estimate = request.Estimate,
                Attachement = request.Attachement
            };

            var result = await _cardRepository.UpdateCard(model);

            return result;
        }
    }
}