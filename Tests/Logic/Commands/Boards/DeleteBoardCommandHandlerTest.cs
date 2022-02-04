using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Boards;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Boards
{
    public class DeleteBoardCommandHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var testRequest = new DeleteBoardCommand
            {
                Id = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(false);

            var sut = new DeleteBoardCommandHandler(mockBoardRepository.Object);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new DeleteBoardCommandHandler(mockBoardRepository.Object);

            var testRequest = new DeleteBoardCommand
            {
                Id = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.Id)).ReturnsAsync(true);
            mockBoardRepository.Setup(x => x.DeleteBoard(testRequest.Id)).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
