using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Statuses;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Statuses
{
    public class GetStatusHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_CallsRepoMethodCorrectlyAndReturnCorrectObject()
        {
            // Arrange
            var mockStatusRepository = new Mock<IStatusRepository>();
            var sut = new GetStatusHandler(mockStatusRepository.Object);

            var testRequest = new GetStatusQuery
            {
                Id = Guid.NewGuid()
            };

            mockStatusRepository.Setup(x => x.GetStatusById(testRequest.Id)).ReturnsAsync(new Status { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<Status>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
