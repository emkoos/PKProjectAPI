using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
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
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool?>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository, IUserRepository userRepository, ICardRepository cardRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
        }

        public async Task<bool?> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            if (!await _commentRepository.CommentExist(request.Id))
            {
                throw new NotFoundException("Not Found Comment");
            }

            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new NotFoundException("Not Found User");
            }

            if (!await _cardRepository.CardExist(request.CardId))
            {
                throw new NotFoundException("Not Found Card");
            }

            if (String.IsNullOrWhiteSpace(request.Content))
            {
                request.Content = "";
            }

            var model = new Comment
            {
                Id = request.Id,
                UserEmail = request.UserEmail,
                CardId = request.CardId,
                Content = request.Content,
                Date = DateTime.Now
            };

            var result = await _commentRepository.UpdateComment(model);

            return result;
        }
    }
}
