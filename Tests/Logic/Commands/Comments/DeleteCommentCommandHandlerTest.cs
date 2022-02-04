using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Comments;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Comments
{
    public class DeleteCommentCommandHandlerTest
    {
        private readonly Mock<ICommentRepository> mockCommentRepository;
        private readonly DeleteCommentCommandHandler sut;

        public DeleteCommentCommandHandlerTest()
        {
            mockCommentRepository = new Mock<ICommentRepository>();
            sut = new DeleteCommentCommandHandler(mockCommentRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingComment_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new DeleteCommentCommand
            {
                Id = Guid.NewGuid()
            };

            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Comment");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new DeleteCommentCommand
            {
                Id = Guid.NewGuid()
            };

            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(true);
            mockCommentRepository.Setup(x => x.DeleteComment(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
