using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Cards;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Cards
{
    public class GetColumnCardsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithColumnId_ReturnsCorrectObjects()
        {
            // Arrange
            var mockCardRepository = new Mock<ICardRepository>();
            var sut = new GetColumnCardsHandler(mockCardRepository.Object);

            var testRequest = new GetColumnCardsQuery
            {
                ColumnId = Guid.NewGuid()
            };

            var testResponse = new List<Card>
            {
                new Card {Id = Guid.NewGuid(), Title="CArdTest1", ColumnId = testRequest.ColumnId },
                new Card {Id = Guid.NewGuid(), Title="CardTest2", ColumnId = testRequest.ColumnId }
            };

            mockCardRepository.Setup(x => x.GetCardsByColumnId(testRequest.ColumnId)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Card>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
