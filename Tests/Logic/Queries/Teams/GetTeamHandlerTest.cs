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
    public class GetTeamHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_CallsRepoMethodCorrectlyAndReturnCorrectObject()
        {
            // Arrange
            var mockTeamRepository = new Mock<ITeamRepository>();
            var sut = new GetTeamHandler(mockTeamRepository.Object);

            var testRequest = new GetTeamQuery
            {
                Id = Guid.NewGuid(),
                Name = "TeamTest"
            };

            mockTeamRepository.Setup(x => x.GetTeamById(testRequest.Id)).ReturnsAsync(new Team { Id = testRequest.Id, Name = testRequest.Name });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<Team>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
