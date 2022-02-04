using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Teams;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Teams
{
    public class CreateTeamCommandHandlerTest
    {
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly CreateTeamCommandHandler sut;

        public CreateTeamCommandHandlerTest()
        {
            mockTeamRepository = new Mock<ITeamRepository>();
            sut = new CreateTeamCommandHandler(mockTeamRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithoutUserEmail_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateTeamCommand();

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Something wrong with creating User");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new CreateTeamCommand
            {
                Name = "test",
                UserEmail = "test@test.com"
            };

            mockTeamRepository.Setup(x => x.CreateTeam(It.IsAny<Team>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
