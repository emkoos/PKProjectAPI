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
    public class GetMyAllBoardsHandler : IRequestHandler<GetMyAllBoardsQuery, IEnumerable<Board>>
    {
        private readonly IBoardRepository _repository;

        public GetMyAllBoardsHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Board>> Handle(GetMyAllBoardsQuery request, CancellationToken cancellationToken)
        {
            if (request.Email is null)
            {
                throw new Exception("Authentication error");
            }

            var boards = await _repository.GetUserAllBoards(request.Email);

            if (boards is null)
            {
                throw new Exception("No Boards for this user");
            }

            return boards;
        }
    }
}
