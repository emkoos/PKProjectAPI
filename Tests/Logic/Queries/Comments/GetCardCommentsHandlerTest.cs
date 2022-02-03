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
    public class GetCardCommentsHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithCorrectCardId_ReturnsCorrectObjects()
        {
            // Arrange
            var mockCommentRepository = new Mock<ICommentRepository>();
            var sut = new GetCardCommentsHandler(mockCommentRepository.Object);

            var testRequest = new GetCardCommentsQuery
            {
                CardId = Guid.NewGuid()
            };

            var testResponse = new List<Comment>
            {
                new Comment {Id = Guid.NewGuid(), Content="CommentTest1", CardId = testRequest.CardId },
                new Comment  {Id = Guid.NewGuid(), Content="ComentTest2", CardId = testRequest.CardId }
            };

            mockCommentRepository.Setup(x => x.GetCommentsByCardId(testRequest.CardId)).ReturnsAsync(testResponse);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<List<Comment>>();
            result.Should().BeSameAs(testResponse);
        }
    }
}
