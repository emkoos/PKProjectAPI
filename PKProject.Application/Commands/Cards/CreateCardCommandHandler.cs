using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Cards
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, bool>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserRepository _userRepository;

        public CreateCardCommandHandler(ICardRepository cardRepository, IUserRepository userRepository)
        {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new Exception("Not Found User");
            }

            if (String.IsNullOrWhiteSpace(request.Description))
            {
                request.Description = "";
            }

            var model = new Card
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                UserEmail = request.UserEmail,
                ColumnId = request.ColumnId,
                StatusId = request.StatusId,
                CreatedDate = DateTime.Now,
                UpdatedStatusDoneDate = DateTime.MinValue,
                DeadlineDate = request.DeadlineDate.AddHours(1),
                Priority = request.Priority,
                Estimate = request.Estimate,
                Attachement = request.Attachement
            };

            var result = await _cardRepository.CreateCard(model);

            return result;
        }
    }
}
