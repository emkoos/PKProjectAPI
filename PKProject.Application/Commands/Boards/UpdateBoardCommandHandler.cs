﻿using MediatR;
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
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, bool?>
    {
        public readonly IMediator _mediator;
        public readonly ITeamRepository _teamRepository;
        public readonly IBoardRepository _boardRepository;
        public readonly IBoardTypeRepository _boardTypeRepository;

        public UpdateBoardCommandHandler(IMediator mediator, ITeamRepository teamRepository, IBoardRepository boardRepository, IBoardTypeRepository boardTypeRepository)
        {
            _mediator = mediator;
            _teamRepository = teamRepository;
            _boardRepository = boardRepository;
            _boardTypeRepository = boardTypeRepository;
        }

        public async Task<bool?> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            if (!await _boardRepository.BoardExist(request.Id))
            {
                throw new Exception("Not Found Board");
            }

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
                Id = request.Id,
                Name = request.Name,
                TeamId = request.TeamId,
                BoardTypeId = request.BoardTypeId
            };

            var result = await _boardRepository.UpdateBoard(model);

            return result;
        }
    }
}