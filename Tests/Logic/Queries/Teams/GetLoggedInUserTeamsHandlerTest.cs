using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Teams;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Teams
{
    public class GetLoggedInUserTeamsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectUserEmail_ReturnsCorrectObjects()
        {
            // Arrange
            var mockTeamRepository = new Mock<ITeamRepository>();
            var sut = new GetLoggedInUserTeamsHandler(mockTeamRepository.Object);

            var testRequest = new GetLoggedInUserTeamsQuery
            {
                Email = "test@test.com"
            };

            var testResponse = new List<Team>
            {
                new Team {Id = Guid.NewGuid(), Name="teamTest1" },
                new Team  {Id = Guid.NewGuid(), Name="teamTest2" }
            };

            mockTeamRepository.Setup(x => x.GetLoggedInUserTeams(testRequest.Email)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Team>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
