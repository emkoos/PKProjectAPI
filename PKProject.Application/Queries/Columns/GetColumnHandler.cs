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
    public class GetColumnHandler : IRequestHandler<GetColumnQuery, Column>
    {
        private readonly IColumnRepository _repository;

        public GetColumnHandler(IColumnRepository repository)
        {
            _repository = repository;
        }

        public async Task<Column> Handle(GetColumnQuery request, CancellationToken cancellationToken)
        {
            var column = await _repository.GetColumnById(request.Id);

            return column;
        }
    }
}
