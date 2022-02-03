using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Comments;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Comments
{
    public class GetCommentHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_CallsRepoMethodCorrectlyAndReturnCorrectObject()
        {
            // Arrange
            var mockCommentRepository = new Mock<ICommentRepository>();
            var sut = new GetCommentHandler(mockCommentRepository.Object);

            var testRequest = new GetCommentQuery
            {
                Id = Guid.NewGuid()
            };

            mockCommentRepository.Setup(x => x.GetCommentById(testRequest.Id)).ReturnsAsync(new Comment { Id = testRequest.Id });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<Comment>();
            result.Id.Should().Be(testRequest.Id);
        }
    }
}
