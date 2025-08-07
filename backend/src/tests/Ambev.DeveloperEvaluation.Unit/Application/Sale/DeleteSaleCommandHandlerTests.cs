using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Tests.Sales
{
    public class DeleteSaleCommandHandlerTests
    {
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly Mock<ISaleItemRepository> _saleItemRepositoryMock;
        private readonly DeleteSaleCommandHandler _handler;

        public DeleteSaleCommandHandlerTests()
        {
            _publishEndpointMock = new Mock<IPublishEndpoint>();
            _saleItemRepositoryMock = new Mock<ISaleItemRepository>();

            _handler = new DeleteSaleCommandHandler(
                _saleItemRepositoryMock.Object,
                _publishEndpointMock.Object
            );
        }

        [Fact(DisplayName = "Should delete sale item when it exists")]
        public async Task Handle_Should_DeleteSaleItem_When_Exists()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var saleItem = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product X", 2, 10m, 0);

            _saleItemRepositoryMock.Setup(r => r.GetByIdAsync(itemId, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(saleItem);
            _saleItemRepositoryMock.Setup(r => r.DeleteAsync(itemId, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(true);

            var command = new DeleteSaleCommand(itemId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success, $"Expected Success true but got {result.Success}. Message: {result.Message}");
            Assert.Equal("Item deleted successfully.", result.Message);
            _saleItemRepositoryMock.Verify(r => r.DeleteAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);

            // Verifica publicação do evento
            _publishEndpointMock.Verify(p => p.Publish(It.IsAny<SaleItemDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Should return not found when item does not exist")]
        public async Task Handle_Should_ReturnNotFound_When_ItemDoesNotExist()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            _saleItemRepositoryMock.Setup(r => r.GetByIdAsync(itemId, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync((SaleItem)null);

            var command = new DeleteSaleCommand(itemId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("not found", result.Message);
            _saleItemRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
            _publishEndpointMock.Verify(p => p.Publish(It.IsAny<SaleItemDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Should return failure when delete fails")]
        public async Task Handle_Should_ReturnFailure_When_DeleteFails()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var saleItem = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product X", 2, 10m, 0);

            _saleItemRepositoryMock.Setup(r => r.GetByIdAsync(itemId, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(saleItem);
            _saleItemRepositoryMock.Setup(r => r.DeleteAsync(itemId, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(false);

            var command = new DeleteSaleCommand(itemId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Failed to delete item.", result.Message);
            _saleItemRepositoryMock.Verify(r => r.DeleteAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
            _publishEndpointMock.Verify(p => p.Publish(It.IsAny<SaleItemDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
