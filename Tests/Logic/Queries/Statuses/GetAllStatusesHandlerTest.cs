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
    public class GetAllStatusesHandlerTest
    {
        [Fact]
        public async Task Handler_CorrectRequestObject_CallsRepoMethodCorrectly()
        {
            // Arrange
            var mockStatusRepository = new Mock<IStatusRepository>();
            var sut = new GetAllStatusesHandler(mockStatusRepository.Object);

            var testResponse = new List<Status>
            {
                new Status {Id = Guid.NewGuid(), Name="StatusTest1" },
                new Status {Id = Guid.NewGuid(), Name="StatusTest2" }
            };

            mockStatusRepository.Setup(x => x.GetAllStatuses()).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(new GetAllStatusesQuery(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Status>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
