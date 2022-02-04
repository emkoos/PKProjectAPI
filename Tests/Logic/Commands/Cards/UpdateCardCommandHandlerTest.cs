using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Cards;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Cards
{
    public class UpdateCardCommandHandlerTest
    {
        private readonly Mock<ICardRepository> mockCardRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IStatusRepository> mockStatusRepository;
        private readonly UpdateCardCommandHandler sut;

        public UpdateCardCommandHandlerTest()
        {
            mockCardRepository = new Mock<ICardRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockStatusRepository = new Mock<IStatusRepository>();
            sut = new UpdateCardCommandHandler(mockCardRepository.Object, mockUserRepository.Object, mockStatusRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingCard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateCardCommand
            {
                Id = Guid.NewGuid()
            };

            mockCardRepository.Setup(x => x.CardExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Card");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateCardCommand
            {
                UserEmail = "test@test.com"
            };

            mockCardRepository.Setup(x => x.CardExist(testRequest.Id)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found User");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new UpdateCardCommand
            {
                Id = Guid.NewGuid(),
                Title = "test",
                Description = "testtest",
                UserEmail = "test@test.com",
                ColumnId = Guid.NewGuid(),
                StatusId = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(-2),
                UpdatedStatusDoneDate = DateTime.Now.AddDays(-1),
                DeadlineDate = DateTime.Now.AddDays(5),
                Priority = 1,
                Estimate = 3,
                Attachement = null
            };

            var testRequestModel = new Card
            {
                Id = testRequest.Id,
                Title = testRequest.Title,
                Description = testRequest.Description,
                UserEmail = testRequest.UserEmail,
                ColumnId = testRequest.ColumnId,
                StatusId = testRequest.StatusId,
                DeadlineDate = testRequest.DeadlineDate,
                Priority = testRequest.Priority,
                Estimate = testRequest.Estimate,
                Attachement = testRequest.Attachement
            };

            var testStatus = new Status
            {
                Id = Guid.NewGuid(),
                Name = "Done"
            };

            mockCardRepository.Setup(x => x.GetCardById(testRequest.Id)).ReturnsAsync(testRequestModel);

            mockStatusRepository.Setup(x => x.GetStatusById(testRequest.StatusId)).ReturnsAsync(testStatus);

            mockCardRepository.Setup(x => x.CardExist(testRequest.Id)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CreateCard(testRequestModel)).ReturnsAsync(true);

            mockCardRepository.Setup(x => x.UpdateCard(It.IsAny<Card>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    
    }
}
