using MediatR;
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
        private readonly IMediator _mediator;
        private readonly ICardRepository _cardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IStatusRepository _statusRepository;

        public UpdateCardCommandHandler(IMediator mediator, ICardRepository cardRepository, IUserRepository userRepository, IBoardRepository boardRepository, IStatusRepository statusRepository)
        {
            _mediator = mediator;
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _boardRepository = boardRepository;
            _statusRepository = statusRepository;
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

            if (String.IsNullOrWhiteSpace(request.Description))
            {
                request.Description = "";
            }

            var UpdateDate = request.UpdatedStatusDoneDate;

            var editingCard = await _cardRepository.GetCardById(request.Id);

            var statusBefore = await _statusRepository.GetStatusById(editingCard.StatusId);
            var statusNew = await _statusRepository.GetStatusById(request.StatusId);
            if (statusNew.Name == "Done" && statusBefore.Name != "Done")
            {
                UpdateDate = DateTime.Now;
            }

            if (statusBefore.Name == "Done" && statusNew.Name != "Done")
            {
                UpdateDate = DateTime.MinValue;
            }

            var model = new Card
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                UserEmail = request.UserEmail,
                ColumnId = request.ColumnId,
                StatusId = request.StatusId,
                CreatedDate = request.CreatedDate,
                UpdatedStatusDoneDate = UpdateDate,
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
