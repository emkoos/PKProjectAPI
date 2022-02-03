using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Cards;
using PKProject.Application.Queries.Columns;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Column
{
    public class GetBoardColumnsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectBoardId_ReturnsCorrectObjects()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var sut = new GetBoardColumnsHandler(mockColumnRepository.Object);

            var testRequest = new GetBoardColumnsQuery
            {
                BoardId = Guid.NewGuid()
            };

            var testResponse = new List<PKProject.Domain.Models.Column>
            {
                new PKProject.Domain.Models.Column {Id = Guid.NewGuid(), Title="ColTest1", BoardId = testRequest.BoardId },
                new PKProject.Domain.Models.Column  {Id = Guid.NewGuid(), Title="ColTest2", BoardId = testRequest.BoardId }
            };

            mockColumnRepository.Setup(x => x.GetColumnsByBoardId(testRequest.BoardId)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<PKProject.Domain.Models.Column>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
