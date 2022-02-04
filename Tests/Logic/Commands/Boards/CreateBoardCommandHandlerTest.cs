using FluentAssertions;
using MediatR;
using Moq;
using PKProject.Application.Commands.Boards;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Boards
{
    public class CreateBoardCommandHandlerTest
    {
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IBoardRepository> mockBoardRepository;
        private readonly Mock<IBoardTypeRepository> mockBoardTypeRepository;
        private readonly CreateBoardCommandHandler sut;

        public CreateBoardCommandHandlerTest()
        {
            mockTeamRepository = new Mock<ITeamRepository>();
            mockBoardRepository = new Mock<IBoardRepository>();
            mockBoardTypeRepository = new Mock<IBoardTypeRepository>();
            sut = new CreateBoardCommandHandler(mockTeamRepository.Object, mockBoardRepository.Object, mockBoardTypeRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingTeam_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateBoardCommand
            {
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(false);

            // Act
            Func<Task<Guid>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Team");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoardType_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateBoardCommand
            {
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockBoardTypeRepository.Setup(x => x.BoardTypeExist(testRequest.BoardTypeId)).ReturnsAsync(false);

            // Act
            Func<Task<Guid>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board Type");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsNotEmptyResult()
        {
            // Arrange
            var testRequest = new CreateBoardCommand
            {
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            var testRequestModel = new Board
            {
                Id = Guid.NewGuid(),
                Name = testRequest.Name,
                TeamId = testRequest.TeamId,
                BoardTypeId = testRequest.BoardTypeId
            };

            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockBoardTypeRepository.Setup(x => x.BoardTypeExist(testRequest.BoardTypeId)).ReturnsAsync(true);
            mockBoardRepository.Setup(x => x.CreateBoard(It.IsAny<Board>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
