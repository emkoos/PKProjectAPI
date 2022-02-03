using FluentAssertions;
using Moq;
using PKProject.Application.Queries.Users;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Queries.Users
{
    public class GetLoggedInUserHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithId_ReturnsObjectWithTheSameId()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var sut = new GetLoggedInUserHandler(mockUserRepository.Object);

            var testRequest = new GetLoggedInUserQuery
            {
                UserEmail = "test@test.com"
            };

            mockUserRepository.Setup(x => x.GetLoggedInUser(testRequest.UserEmail)).ReturnsAsync(new User { Email = testRequest.UserEmail });

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<User>();
            result.Email.Should().Be(testRequest.UserEmail);
        }
    }
}
