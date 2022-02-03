using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Cards;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Cards
{
    public class DeleteCardCommandHandlerTest
    {
        private readonly Mock<ICardRepository> mockCardRepository;
        private readonly DeleteCardCommandHandler sut;

        public DeleteCardCommandHandlerTest()
        {
            mockCardRepository = new Mock<ICardRepository>();
            sut = new DeleteCardCommandHandler(mockCardRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingCard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new DeleteCardCommand
            {
                Id = Guid.NewGuid()
            };

            mockCardRepository.Setup(x => x.CardExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Card");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new DeleteCardCommand
            {
                Id = Guid.NewGuid()
            };

            mockCardRepository.Setup(x => x.CardExist(testRequest.Id)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.DeleteCard(testRequest.Id)).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
