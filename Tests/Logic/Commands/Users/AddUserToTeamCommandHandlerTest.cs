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
    public class AddUserToTeamCommandHandlerTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IEmailSender> mockEmailSender;
        private readonly AddUserToTeamCommandHandler sut;

        public AddUserToTeamCommandHandlerTest()
        {
            mockTeamRepository = new Mock<ITeamRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockEmailSender = new Mock<IEmailSender>();

            sut = new AddUserToTeamCommandHandler(mockUserRepository.Object, mockTeamRepository.Object, mockEmailSender.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new AddUserToTeamCommand
            {
                TeamId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "User with that email does not exists.");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingTeam_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new AddUserToTeamCommand
            {
                TeamId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Team does not exists.");
        }

        [Fact]
        public async Task Handler_RequestObjectWithAlreadyExistingUserInThatTeam_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new AddUserToTeamCommand
            {
                TeamId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExistInTeam(testRequest.UserEmail, testRequest.TeamId)).ReturnsAsync(true);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotAvailableException>().Where(e => e.Message == "User with that email exists in that team.");
        }

        [Fact]
        public async Task Handler_AddedUserToTeamAndSendedRemind_ReturnsTrue()
        {
            // Arrange
            var testRequest = new AddUserToTeamCommand
            {
                TeamId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            var testResult = new Team
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExistInTeam(testRequest.UserEmail, testRequest.TeamId)).ReturnsAsync(false);

            mockUserRepository.Setup(x => x.AddUserToTeam(testRequest.UserEmail, testRequest.TeamId)).ReturnsAsync(true);

            mockTeamRepository.Setup(x => x.GetTeamById(testRequest.TeamId)).ReturnsAsync(testResult);

            mockEmailSender.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_NotAddedUserToTeam_ReturnsFalse()
        {
            // Arrange
            var testRequest = new AddUserToTeamCommand
            {
                TeamId = Guid.NewGuid(),
                UserEmail = "test@test.com"
            };

            var testResult = new Team
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.TeamExist(testRequest.TeamId)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExistInTeam(testRequest.UserEmail, testRequest.TeamId)).ReturnsAsync(false);

            mockUserRepository.Setup(x => x.AddUserToTeam(testRequest.UserEmail, testRequest.TeamId)).ReturnsAsync(false);

            mockTeamRepository.Setup(x => x.GetTeamById(testRequest.TeamId)).ReturnsAsync(testResult);

            mockEmailSender.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeFalse();
        }
    }
}
