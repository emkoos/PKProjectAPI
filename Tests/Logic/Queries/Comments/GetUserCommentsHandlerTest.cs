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
    public class GetUserCommentsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectUserEmail_ReturnsCorrectObjects()
        {
            // Arrange
            var mockCommentRepository = new Mock<ICommentRepository>();
            var sut = new GetUserCommentsHandler(mockCommentRepository.Object);

            var testRequest = new GetUserCommentsQuery
            {
                Email = "test@test.com"
            };

            var testResponse = new List<Comment>
            {
                new Comment {Id = Guid.NewGuid(), Content="CommentTest1", UserEmail = testRequest.Email },
                new Comment  {Id = Guid.NewGuid(), Content="ComentTest2", UserEmail = testRequest.Email }
            };

            mockCommentRepository.Setup(x => x.GetCommentsByUserEmail(testRequest.Email)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Comment>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
