using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Users;
using PKProject.Domain.Exceptions.AppExceptions;
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
    public class UpdateUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly UpdateUserCommandHandler sut;

        public UpdateUserCommandHandlerTest()
        {
            mockUserRepository = new Mock<IUserRepository>();
            sut = new UpdateUserCommandHandler(mockUserRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateUserCommand
            {
                Email = "test@test.com"
            };

            mockUserRepository.Setup(x => x.GetLoggedInUser(testRequest.Email)).ReturnsAsync(It.IsAny<User>());
            mockUserRepository.Setup(x => x.UserExist(testRequest.Email)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found User");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new UpdateUserCommand
            {
                Email = "test@test.com",
                Username = "test",
                Firstname = "test",
                Lastname = "test",
            };

            var testResult = new User
            {
                Email = "test@test.com",
                Username = "test",
                Firstname = "test",
                Lastname = "test",
                Photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
        };

            mockUserRepository.Setup(x => x.GetLoggedInUser(testRequest.Email)).ReturnsAsync(testResult);
            mockUserRepository.Setup(x => x.UserExist(testRequest.Email)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
