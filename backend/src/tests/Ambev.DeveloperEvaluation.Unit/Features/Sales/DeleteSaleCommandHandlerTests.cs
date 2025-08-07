using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class DeleteSaleCommandHandlerTests
    {
        [Fact(DisplayName = "Handle deve deletar venda sem lançar exceção")]
        public async Task Handle_Should_Delete_Sale()
        {
            // Arrange
            var saleItemRepositoryMock = new Mock<ISaleItemRepository>();
            var publishEndpointMock = new Mock<IPublishEndpoint>();
            var handler = new DeleteSaleCommandHandler(saleItemRepositoryMock.Object, publishEndpointMock.Object);

            var command = new DeleteSaleCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
