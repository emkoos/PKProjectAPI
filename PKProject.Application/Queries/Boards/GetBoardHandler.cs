using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Boards
{
    public class GetBoardHandler : IRequestHandler<GetBoardQuery, Board>
    {
        private readonly IBoardRepository _repository;

        public GetBoardHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Board> Handle(GetBoardQuery request, CancellationToken cancellationToken)
        {
            var board = await _repository.GetBoardById(request.Id);

            return board;
        }
    }
}
