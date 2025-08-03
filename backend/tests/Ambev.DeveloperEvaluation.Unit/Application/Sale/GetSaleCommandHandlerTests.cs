using Xunit;
using Moq;
using AutoMapper;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Tests.Sales
{
    public class GetSaleCommandHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetSaleCommandHandler _handler;

        public GetSaleCommandHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetSaleCommandHandler(_saleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Handle deve retornar venda quando ela existir")]
        public async Task Handle_Should_ReturnSale_When_Exists()
        {
            // Arrange
            var customer = new Customer("Customer 1", "customer@email.com", "11999999999", "Address");
            var sale = new Sale("S1", DateTime.UtcNow, customer, "Main Branch", "Credit Card");

            _saleRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);

            _mapperMock
                .Setup(m => m.Map<GetSaleCommandResult>(It.IsAny<Sale>()))
                .Returns((Sale s) => new GetSaleCommandResult { Id = s.Id });

            var command = new GetSaleCommand(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(m => m.Map<GetSaleCommandResult>(It.IsAny<Sale>()), Times.Once);
        }
    }
}
