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
    public class UpdateCommentCommandHandlerTest
    {
        private readonly Mock<ICommentRepository> mockCommentRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<ICardRepository> mockCardRepository;
        private readonly UpdateCommentCommandHandler sut;
        private readonly UpdateCommentCommand testRequest;

        public UpdateCommentCommandHandlerTest()
        {
            mockCommentRepository = new Mock<ICommentRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockCardRepository = new Mock<ICardRepository>();
            sut = new UpdateCommentCommandHandler(mockCommentRepository.Object, mockUserRepository.Object, mockCardRepository.Object);

            testRequest = new UpdateCommentCommand
            {
                Id = Guid.NewGuid(),
                Content = "test",
                UserEmail = "test@test.com",
                CardId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingComment_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Comment");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingUser_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found User");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingCard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CardExist(testRequest.CardId)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Card");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            mockCommentRepository.Setup(x => x.CommentExist(testRequest.Id)).ReturnsAsync(true);
            mockUserRepository.Setup(x => x.UserExist(testRequest.UserEmail)).ReturnsAsync(true);
            mockCardRepository.Setup(x => x.CardExist(testRequest.CardId)).ReturnsAsync(true);

            mockCommentRepository.Setup(x => x.UpdateComment(It.IsAny<Comment>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
