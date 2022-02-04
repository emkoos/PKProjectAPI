using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Cards
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, bool>
    {
        private readonly ICardRepository _repository;

        public DeleteCardCommandHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.CardExist(request.Id))
            {
                throw new NotFoundException("Not Found Card");
            }

            var result = await _repository.DeleteCard(request.Id);

            return result;
        }
    }
}
