using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Columns;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Columns
{
    public class UpdateColumnCommandHandlerTest
    {
        private readonly Mock<IColumnRepository> mockColumnRepository;
        private readonly Mock<IBoardRepository> mockBoardRepository;
        private readonly UpdateColumnCommandHandler sut;

        public UpdateColumnCommandHandlerTest()
        {
            mockColumnRepository = new Mock<IColumnRepository>();
            mockBoardRepository = new Mock<IBoardRepository>();
            sut = new UpdateColumnCommandHandler(mockColumnRepository.Object, mockBoardRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingColumn_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateColumnCommand
            {
                Id = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.ColumnExist(testRequest.Id)).ReturnsAsync(false);

            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Column");
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new UpdateColumnCommand
            {
                Id = Guid.NewGuid(),
                BoardId = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.ColumnExist(testRequest.Id)).ReturnsAsync(true);
            mockBoardRepository.Setup(x => x.BoardExist(testRequest.BoardId)).ReturnsAsync(false);


            // Act
            Func<Task<bool?>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new UpdateColumnCommand
            {
                Id = Guid.NewGuid(),
                Title = "",
                BoardId = Guid.NewGuid()
            };

            mockColumnRepository.Setup(x => x.ColumnExist(testRequest.Id)).ReturnsAsync(true);
            mockBoardRepository.Setup(x => x.BoardExist(testRequest.BoardId)).ReturnsAsync(true);

            mockColumnRepository.Setup(x => x.UpdateColumn(It.IsAny<Column>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
