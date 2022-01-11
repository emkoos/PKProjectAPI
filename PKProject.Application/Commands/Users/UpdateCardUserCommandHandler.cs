using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.IServices;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class UpdateCardUserCommandHandler : IRequestHandler<UpdateCardUserCommand, bool?>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IEmailSender _emailSender;

        public UpdateCardUserCommandHandler(IMediator mediator, IUserRepository userRepository, ICardRepository cardRepository, IEmailSender emailSender)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _emailSender = emailSender;
        }

        public async Task<bool?> Handle(UpdateCardUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new Exception("Not Found User");
            }
            if (!await _cardRepository.CardExist(request.cardId))
            {
                throw new Exception("Not Found Card");
            }

            var card = await _cardRepository.GetCardById(request.cardId);

            var model = new Card
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                UserEmail = request.UserEmail,
                ColumnId = card.ColumnId,
                StatusId = card.StatusId,
                DeadlineDate = card.DeadlineDate,
                Priority = card.Priority,
                Estimate = card.Estimate,
                Attachement = card.Attachement
            };


            var result = await _cardRepository.UpdateCard(model);

            if (result == true)
            {
                string subject = $"Wybrano Cię do zadania {card.Title}.";
                string toUserEmail = request.UserEmail;
                string text = $"Cześć!<br />" +
                                $"Jesteś od teraz wykonawcą karty {card.Title} w aplikacji PkProjectApp. <br />" +
                                $"Data ukończenia karty ustawiona jest na {card.DeadlineDate} <br />" +
                                $"Zobacz zmiany w aplikacji <a href='#'>PkProjectApp</a> <br />" +
                                $"Zespół PkProjectApp <br />" +
                                $"Miłego dnia!";

                await _emailSender.SendEmail(subject, toUserEmail, text);
            }

            return result;
        }
    }
}
