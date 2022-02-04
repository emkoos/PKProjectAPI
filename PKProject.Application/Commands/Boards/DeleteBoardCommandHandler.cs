using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool>
    {
        private readonly IBoardRepository _repository;

        public DeleteBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.BoardExist(request.Id))
            {
                throw new NotFoundException("Not Found Board");
            }

            var result = await _repository.DeleteBoard(request.Id);

            return result;
        }
    }
}
