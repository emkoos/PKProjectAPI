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
    public class GetUsersByTeamIdHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectTeamId_ReturnsCorrectObjects()
        {
            // Arrange
            var mockTeamRepository = new Mock<ITeamRepository>();
            var sut = new GetUsersByTeamIdHandler(mockTeamRepository.Object);

            var testRequest = new GetUsersByTeamIdQuery
            {
                TeamId = Guid.NewGuid()
            };

            var testResponse = new List<User>
            {
                new User { Email = "test1@gmail.com" },
                new User  { Email = "test2@gmail.com" }
            };

            mockTeamRepository.Setup(x => x.GetUsersByTeamId(testRequest.TeamId)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<User>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
