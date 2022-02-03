using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Boards;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Boards
{
    public class GetBoardHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_ReturnsObjectWithTheSameId()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new GetBoardHandler(mockBoardRepository.Object);

            var testRequest = new GetBoardQuery
            {
                Id = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.GetBoardById(testRequest.Id)).ReturnsAsync(new Board { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<Board>();
            result.Id.Should().Be(testRequest.Id);

        }
    }
}
