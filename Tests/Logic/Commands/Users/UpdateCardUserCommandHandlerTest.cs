using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Users;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.IServices;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Users
{
    public class UpdateCardUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<ICardRepository> mockCardRepository;
        private readonly Mock<IEmailSender> mockEmailSender;
        private readonly UpdateCardUserCommandHandler sut;

        public UpdateCardUserCommandHandlerTest()
        {
            mockCardRepository = new Mock<ICardRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockEmailSender = new Mock<IEmailSender>();

            sut = new UpdateCardUserCommandHandler(mockUserRepository.Object, mockCardRepository.Object, mockEmailSender.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateCardUserCommand
            {
                cardId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found User");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingCard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateCardUserCommand
            {
                cardId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CardExist(testRequest.cardId)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Card");
        }

        [Fact]
        public async Task Handler_UpdatedCardUserAndSendedRemind_ReturnsTrue()
        {
            // Arrange
            var testRequest = new UpdateCardUserCommand
            {
                cardId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            var testResult = new Card
            {
                Title = "testTitle",
                DeadlineDate = DateTime.Now.AddDays(3)
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CardExist(testRequest.cardId)).ReturnsAsync(true);

            mockCardRepository.Setup(x => x.GetCardById(testRequest.cardId)).ReturnsAsync(testResult);
            mockCardRepository.Setup(x => x.UpdateCard(It.IsAny<Card>())).ReturnsAsync(true);

            mockEmailSender.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_NotUpdatedCardUser_ReturnsFalse()
        {
            // Arrange
            var testRequest = new UpdateCardUserCommand
            {
                cardId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            var testResult = new Card
            {
                Title = "testTitle",
                DeadlineDate = DateTime.Now.AddDays(3)
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CardExist(testRequest.cardId)).ReturnsAsync(true);

            mockCardRepository.Setup(x => x.GetCardById(testRequest.cardId)).ReturnsAsync(testResult);
            mockCardRepository.Setup(x => x.UpdateCard(It.IsAny<Card>())).ReturnsAsync(false);

            mockEmailSender.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeFalse();
        }
    }
}

