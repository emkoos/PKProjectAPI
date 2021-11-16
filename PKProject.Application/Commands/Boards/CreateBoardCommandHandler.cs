using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, bool>
    {
        public readonly IMediator _mediator;
        public readonly ITeamRepository _teamRepository;
        public readonly IBoardRepository _boardRepository;
        public readonly IBoardTypeRepository _boardTypeRepository;

        public CreateBoardCommandHandler(IMediator mediator, ITeamRepository teamRepository, IBoardRepository boardRepository, IBoardTypeRepository boardTypeRepository)
        {
            _mediator = mediator;
            _teamRepository = teamRepository;
            _boardRepository = boardRepository;
            _boardTypeRepository = boardTypeRepository;
        }

        public async Task<bool> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {

            if (!await _teamRepository.TeamExist(request.TeamId))
            {
                throw new Exception("Not Found Team");
            }

            if (!await _boardTypeRepository.BoardTypeExist(request.BoardTypeId))
            {
                throw new Exception("Not Found Board Type");
            }

            var model = new Board
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                TeamId = request.TeamId,
                BoardTypeId = request.BoardTypeId
            };

            var result = await _boardRepository.CreateBoard(model);

            return result;
        }
    }
}
