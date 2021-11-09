using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.BoardTypes
{
    public class GetBoardTypeHandler : IRequestHandler<GetBoardTypeQuery, BoardType>
    {
        private readonly IBoardTypeRepository _repository;

        public GetBoardTypeHandler(IBoardTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<BoardType> Handle(GetBoardTypeQuery request, CancellationToken cancellationToken)
        {
            var boardType = await _repository.GetBoardTypeById(request.Id);

            return boardType;
        }
    }
}
