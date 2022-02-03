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
    public class GetCardHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_ReturnsObjectWithTheSameId()
        {
            // Arrange
            var mockCardRepository = new Mock<ICardRepository>();
            var sut = new GetCardHandler(mockCardRepository.Object);

            var testRequest = new GetCardQuery
            {
                Id = Guid.NewGuid()
            };

            mockCardRepository.Setup(x => x.GetCardById(testRequest.Id)).ReturnsAsync(new Card { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<Card>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
