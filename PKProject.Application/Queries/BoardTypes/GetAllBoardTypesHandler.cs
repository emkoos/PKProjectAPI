using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKProject.Domain.IRepositories;
using System.Threading;
using PKProject.Domain.Models;

namespace PKProject.Application.Queries.BoardTypes
{
    public class GetAllBoardTypesHandler : IRequestHandler<GetAllBoardTypesQuery, IEnumerable<BoardType>>
    {
        private readonly IBoardTypeRepository _repository;

        public GetAllBoardTypesHandler(IBoardTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BoardType>> Handle(GetAllBoardTypesQuery request, CancellationToken cancellationToken)
        {
            var boardTypesList = await _repository.GetAllBoardTypes();

            return boardTypesList;
        }
    }
}
