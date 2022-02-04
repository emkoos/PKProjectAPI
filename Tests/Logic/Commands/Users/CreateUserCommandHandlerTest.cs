using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Users;
using PKProject.Domain.IRepositories;
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
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly CreateUserCommandHandler sut;

        public CreateUserCommandHandlerTest()
        {
            mockUserRepository = new Mock<IUserRepository>();
            sut = new CreateUserCommandHandler(mockUserRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithoutUserEmail_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateUserCommand
            {
               Email = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.Email)).ReturnsAsync(true);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "User with that email already exists.");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new CreateUserCommand
            {
                Email = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.Email)).ReturnsAsync(false);
            mockUserRepository.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
