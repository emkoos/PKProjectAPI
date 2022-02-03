using FluentAssertions;
using Moq;
using PKProject.Application.Queries.BoardTypes;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.BoardTypes
{
    public class GetBoardTypeHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_ReturnsObjectWithTheSameId()
        {
            // Arrange
            var mockBoardTypeRepository = new Mock<IBoardTypeRepository>();
            var sut = new GetBoardTypeHandler(mockBoardTypeRepository.Object);

            var testRequest = new GetBoardTypeQuery
            {
                Id = Guid.NewGuid()
            };

            mockBoardTypeRepository.Setup(x => x.GetBoardTypeById(testRequest.Id)).ReturnsAsync(new BoardType { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<BoardType>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
