using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Columns;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Columns
{
    public class DeleteColumnCommandHandlerTest
    {
        [Fact]
        public async Task Handler_RequestObjectWithNotExistingColumn_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var testRequest = new DeleteColumnCommand
            {
                Id = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.ColumnExist(testRequest.Id)).ReturnsAsync(false);

            var sut = new DeleteColumnCommandHandler(mockColumnRepository.Object);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<Exception>().Where(e => e.Message == "Not Found Column");
        }


        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var sut = new DeleteColumnCommandHandler(mockColumnRepository.Object);

            var testRequest = new DeleteColumnCommand
            {
                Id = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.ColumnExist(testRequest.Id)).ReturnsAsync(true);
            mockColumnRepository.Setup(x => x.DeleteColumn(testRequest.Id)).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
