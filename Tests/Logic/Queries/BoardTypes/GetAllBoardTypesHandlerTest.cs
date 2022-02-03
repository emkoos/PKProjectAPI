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
    public class GetAllBoardTypesHandlerTest
    {
        [Fact]
        public async Task Handler_CorrectRequestObject_CallsRepoMethodCorrectly()
        {
            // Arrange
            var mockBoardTypeRepository = new Mock<IBoardTypeRepository>();
            var sut = new GetAllBoardTypesHandler(mockBoardTypeRepository.Object);

            var testResponse = new List<BoardType>
            {
                new BoardType {Id = Guid.NewGuid(), Name="BoardTypeTest1" },
                new BoardType {Id = Guid.NewGuid(), Name="BoardTypeTest2" }
            };

            mockBoardTypeRepository.Setup(x => x.GetAllBoardTypes()).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(new GetAllBoardTypesQuery(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<BoardType>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
