using FluentAssertions;
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
    public class UpdateBoardCommandHandlerTest
    {
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IBoardRepository> mockBoardRepository;
        private readonly Mock<IBoardTypeRepository> mockBoardTypeRepository;
        private readonly UpdateBoardCommandHandler sut;

        public UpdateBoardCommandHandlerTest()
        {
            mockTeamRepository = new Mock<ITeamRepository>();
            mockBoardRepository = new Mock<IBoardRepository>();
            mockBoardTypeRepository = new Mock<IBoardTypeRepository>();
            sut = new UpdateBoardCommandHandler(mockTeamRepository.Object, mockBoardRepository.Object, mockBoardTypeRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateBoardCommand
            {
                Id = Guid.NewGuid(),
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingTeam_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateBoardCommand
            {
                Id = Guid.NewGuid(),
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Team");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoardType_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateBoardCommand
            {
                Id = Guid.NewGuid(),
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockBoardTypeRepository.Setup(x => x.BoardTypeExist(testRequest.TeamId)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board Type");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new UpdateBoardCommand
            {
                Id = Guid.NewGuid(),
                Name = "BoardTest",
                TeamId = Guid.NewGuid(),
                BoardTypeId = Guid.NewGuid()
            };

            var testRequestModel = new Board
            {
                Id = testRequest.Id,
                Name = testRequest.Name,
                TeamId = testRequest.TeamId,
                BoardTypeId = testRequest.BoardTypeId
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockBoardTypeRepository.Setup(x => x.BoardTypeExist(testRequest.BoardTypeId)).ReturnsAsync(true);

            mockBoardRepository.Setup(x => x.UpdateBoard(It.IsAny<Board>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
