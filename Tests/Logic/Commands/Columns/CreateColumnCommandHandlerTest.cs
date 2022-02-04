using FluentAssertions;
using Moq;
using PKProject.Application.Commands.Columns;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using PKProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Logic.Commands.Columns
{
    public class CreateColumnCommandHandlerTest
    {
        private readonly Mock<IColumnRepository> mockColumnRepository;
        private readonly Mock<IBoardRepository> mockBoardRepository;
        private readonly CreateColumnCommandHandler sut;

        public CreateColumnCommandHandlerTest()
        {
            mockColumnRepository = new Mock<IColumnRepository>();
            mockBoardRepository = new Mock<IBoardRepository>();
            sut = new CreateColumnCommandHandler(mockColumnRepository.Object, mockBoardRepository.Object);
        }

        [Fact]
        public async Task Handler_RequestObjectWithNotExistingBoard_ThrowsExceptionWithCorrectErrorMessage()
        {
            // Arrange
            var testRequest = new CreateColumnCommand
            {
                BoardId = Guid.NewGuid()
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.BoardId)).ReturnsAsync(false);

            // Act
            Func<Task<bool>> act = async () => await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().Where(e => e.Message == "Not Found Board");
        }

        [Fact]
        public async Task Handler_RequestObjectWithCorrectData_ReturnsTrue()
        {
            // Arrange
            var testRequest = new CreateColumnCommand
            {
                Title = "test",
                Position = 1,
                BoardId = Guid.NewGuid(),
            };

            mockBoardRepository.Setup(x => x.BoardExist(testRequest.BoardId)).ReturnsAsync(true);
            mockColumnRepository.Setup(z => z.CreateColumn(It.IsAny<Column>())).ReturnsAsync(true);

            // Act
            var result = await sut.Handle(testRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeTrue();
        }
    }
}
