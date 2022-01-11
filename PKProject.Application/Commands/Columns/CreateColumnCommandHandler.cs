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
    public class CreateColumnCommandHandler : IRequestHandler<CreateColumnCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IColumnRepository _columnRepository;
        private readonly IBoardRepository _boardRepository;

        public CreateColumnCommandHandler(IMediator mediator, IColumnRepository columnRepository, IBoardRepository boardRepository)
        {
            _mediator = mediator;
            _columnRepository = columnRepository;
            _boardRepository = boardRepository;
        }

        public async Task<bool> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
        {

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
                Id = Guid.NewGuid(),
                Title = request.Title,
                Position = request.Position,
                BoardId = request.BoardId
            };

            var result = await _columnRepository.CreateColumn(model);

            return result;
        }
    }
}
