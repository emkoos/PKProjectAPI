using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using PKProject.Api.Controllers;
using PKProject.Application.Commands.Teams;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Teams
{
    public class DeleteTeamCommandHandlerTest
    {
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly DeleteTeamCommandHandler sut;

        public DeleteTeamCommandHandlerTest()
        {
            mockTeamRepository = new Mock<ITeamRepository>();
            sut = new DeleteTeamCommandHandler(mockTeamRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistTeam_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new DeleteTeamCommand
            {
                Id = Guid.NewGuid()
            };

            mockTeamRepository.Setup(x => x.TeamExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Team");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new DeleteTeamCommand
            {
                Id = Guid.NewGuid()
            };

            mockTeamRepository.Setup(x => x.TeamExist(testRequest.Id)).ReturnsAsync(true);
            mockTeamRepository.Setup(x => x.DeleteTeam(testRequest.Id)).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
