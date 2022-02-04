using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Boards;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Boards
{
    public class GetMyAllBoardHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithoutEmail_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new GetMyAllBoardsHandler(mockBoardRepository.Object);

            var testRequest = new GetMyAllBoardsQuery
            {
                Email = null
            };

            mockBoardRepository.Setup(x => x.GetUserAllBoards(testRequest.Email)).ReturnsAsync(It.IsAny<IEnumerable<Board>>());

            // Act
            Func<Task<IEnumerable<Board>>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Authentication error");
        }

        [Fact]
        public async Task Handler_RequestObjectUserWithoutBoards_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new GetMyAllBoardsHandler(mockBoardRepository.Object);

            var testRequest = new GetMyAllBoardsQuery
            {
                Email = "test@test.com"
            };

            mockBoardRepository.Setup(x => x.GetUserAllBoards(testRequest.Email)).ReturnsAsync(It.IsAny<IEnumerable<Board>>());

            // Act
            Func<Task<IEnumerable<Board>>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "No Boards for this user");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsCorrectObjects()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new GetMyAllBoardsHandler(mockBoardRepository.Object);

            var testRequest = new GetMyAllBoardsQuery
            {
                Email = "test@test.com"
            };

            var testResponse = new List<Board>
            {
                new Board {Id = Guid.NewGuid(), Name="BoardTest1" },
                new Board {Id = Guid.NewGuid(), Name="BoardTest2" }
            };

            mockBoardRepository.Setup(x => x.GetUserAllBoards(testRequest.Email)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Board>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
