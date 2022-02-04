using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Guid>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IBoardTypeRepository _boardTypeRepository;

        public CreateBoardCommandHandler(ITeamRepository teamRepository, IBoardRepository boardRepository, IBoardTypeRepository boardTypeRepository)
        {
            _teamRepository = teamRepository;
            _boardRepository = boardRepository;
            _boardTypeRepository = boardTypeRepository;
        }

        public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {

            if (!await _teamRepository.TeamExist(request.TeamId))
            {
                throw new NotFoundException("Not Found Team");
            }

            if (!await _boardTypeRepository.BoardTypeExist(request.BoardTypeId))
            {
                throw new NotFoundException("Not Found Board Type");
            }

            var model = new Board
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                TeamId = request.TeamId,
                BoardTypeId = request.BoardTypeId
            };

            var result = await _boardRepository.CreateBoard(model);

            return model.Id;
        }
    }
}
