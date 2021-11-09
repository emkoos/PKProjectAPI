using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, bool>
    {
        public readonly IMediator _mediator;
        public readonly ICommentRepository _commentRepository;
        public readonly IUserRepository _userRepository;
        public readonly ICardRepository _cardRepository;

        public CreateCommentCommandHandler(IMediator mediator, ICommentRepository commentRepository, IUserRepository userRepository, ICardRepository cardRepository)
        {
            _mediator = mediator;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
        }

        public async Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new Exception("Not Found User");
            }

            if (!await _cardRepository.CardExist(request.CardId))
            {
                throw new Exception("Not Found Card");
            }

            if (String.IsNullOrWhiteSpace(request.Content))
            {
                request.Content = "";
            }

            var model = new Comment
            {
                Id = Guid.NewGuid(),
                UserEmail = request.UserEmail,
                CardId = request.CardId,
                Content = request.Content,
                Date = DateTime.Now
            };

            var result = await _commentRepository.CreateComment(model);

            return result;
        }
    }
}
