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
    public class CreateCardCommandHandlerTest
    {
        private readonly Mock<ICardRepository> mockCardRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly CreateCardCommandHandler sut;

        public CreateCardCommandHandlerTest()
        {
            mockCardRepository = new Mock<ICardRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            sut = new CreateCardCommandHandler(mockCardRepository.Object, mockUserRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateCardCommand
            {
                UserEmail = "test@test.com"
            };

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
            var testRequest = new CreateCardCommand
            {
                Title = "test",
                Description = "testtest",
                UserEmail = "test@test.com",
                ColumnId = Guid.NewGuid(),
                StatusId = Guid.NewGuid(),
                DeadlineDate = DateTime.Now.AddDays(5),
                Priority = 1,
                Estimate = 3,
                Attachement = null
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CreateCard(It.IsAny<Card>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
