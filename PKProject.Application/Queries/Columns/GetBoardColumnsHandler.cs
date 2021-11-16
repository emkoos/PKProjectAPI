using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Columns
{
    public class GetBoardColumnsHandler : IRequestHandler<GetBoardColumnsQuery, IEnumerable<Column>>
    {
        private readonly IColumnRepository _repository;

        public GetBoardColumnsHandler(IColumnRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Column>> Handle(GetBoardColumnsQuery request, CancellationToken cancellationToken)
        {
            var columns = await _repository.GetColumnsByBoardId(request.BoardId);

            return columns;
        }
    }
}
