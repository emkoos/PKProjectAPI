using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Columns;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Column
{
    public class GetColumnHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_CallsRepoMethodCorrectlyAndReturnCorrectObject()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var sut = new GetColumnHandler(mockColumnRepository.Object);

            var testRequest = new GetColumnQuery
            {
                Id = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.GetColumnById(testRequest.Id)).ReturnsAsync(new PKProject.Domain.Models.Column { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<PKProject.Domain.Models.Column>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
