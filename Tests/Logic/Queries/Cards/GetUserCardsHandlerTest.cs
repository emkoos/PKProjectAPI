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
    public class GetUserCardsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectUserEmail_ReturnsCorrectObjects()
        {
            // Arrange
            var mockCardRepository = new Mock<ICardRepository>();
            var sut = new GetUserCardsHandler(mockCardRepository.Object);

            var testRequest = new GetUserCardsQuery
            {
                Email = "test@test.com"
            };

            var testResponse = new List<Card>
            {
                new Card {Id = Guid.NewGuid(), Title="CArdTest1", UserEmail = testRequest.Email },
                new Card {Id = Guid.NewGuid(), Title="CardTest2", UserEmail = testRequest.Email }
            };

            mockCardRepository.Setup(x => x.GetCardsByUserEmail(testRequest.Email)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Card>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
