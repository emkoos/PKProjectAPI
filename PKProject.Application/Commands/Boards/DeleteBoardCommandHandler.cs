using MediatR;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool>
    {
        public readonly IBoardRepository _repository;

        public DeleteBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.BoardExist(request.Id))
            {
                throw new Exception("Not Found Board");
            }

            var result = await _repository.DeleteBoard(request.Id);

            return result;
        }
    }
}
