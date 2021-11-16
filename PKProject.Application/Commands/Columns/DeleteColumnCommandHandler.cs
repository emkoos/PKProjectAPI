using MediatR;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Columns
{
    public class DeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand, bool>
    {
        public readonly IColumnRepository _repository;

        public DeleteColumnCommandHandler(IColumnRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.ColumnExist(request.Id))
            {
                throw new Exception("Not Found Column");
            }

            var result = await _repository.DeleteColumn(request.Id);

            return result;
        }
    }
}
