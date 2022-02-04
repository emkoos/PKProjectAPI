using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Columns
{
    public class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand, bool?>
    {
        private readonly IColumnRepository _columnRepository;
        private readonly IBoardRepository _boardRepository;

        public UpdateColumnCommandHandler(IColumnRepository columnRepository, IBoardRepository boardRepository)
        {
            _columnRepository = columnRepository;
            _boardRepository = boardRepository;
        }

        public async Task<bool?> Handle(UpdateColumnCommand request, CancellationToken cancellationToken)
        {
            if (!await _columnRepository.ColumnExist(request.Id))
            {
                throw new Exception("Not Found Column");
            }

            if (!await _boardRepository.BoardExist(request.BoardId))
            {
                throw new Exception("Not Found Board");
            }

            if (String.IsNullOrWhiteSpace(request.Title))
            {
                request.Title = "Karta";
            }

            var model = new Column
            {
                Id = request.Id,
                Title = request.Title,
                Position = request.Position,
                BoardId = request.BoardId
            };

            var result = await _columnRepository.UpdateColumn(model);

            return result;
        }
    }
}
