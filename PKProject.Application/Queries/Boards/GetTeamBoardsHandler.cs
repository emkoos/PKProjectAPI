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
    public class GetTeamBoardsHandler : IRequestHandler<GetTeamBoardsQuery, IEnumerable<Board>>
    {
        private readonly IBoardRepository _repository;

        public GetTeamBoardsHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Board>> Handle(GetTeamBoardsQuery request, CancellationToken cancellationToken)
        {
            var boards = await _repository.GetBoardsByTeamId(request.TeamId);

            return boards;
        }
    }
}
