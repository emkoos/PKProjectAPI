using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Boards;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Boards
{
    public class GetTeamBoardsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithTeamId_ReturnsObjectWithTheSameTeamId()
        {
            // Arrange
            var mockBoardRepository = new Mock<IBoardRepository>();
            var sut = new GetTeamBoardsHandler(mockBoardRepository.Object);

            var testRequest = new GetTeamBoardsQuery
            {
                TeamId = Guid.NewGuid()
            };

            var testResponse = new List<Board>
            {
                new Board {Id = Guid.NewGuid(), Name="BoardTest1", TeamId = testRequest.TeamId },
                new Board {Id = Guid.NewGuid(), Name="BoardTest2", TeamId = testRequest.TeamId }
            };

            mockBoardRepository.Setup(x => x.GetBoardsByTeamId(testRequest.TeamId)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Board>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
