using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Tests.Sales
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPublishEndpoint> _publishMock;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly CreateSaleCommandHandler _handler;

        public CreateSaleCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _publishMock = new Mock<IPublishEndpoint>();
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();

            _handler = new CreateSaleCommandHandler(
                _mapperMock.Object,
                _publishMock.Object,
                _saleRepositoryMock.Object,
                _productRepositoryMock.Object,
                _customerRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Should create sale when valid data is provided")]
        public async Task Handle_Should_CreateSale_When_Valid()
        {
            // Arrange
            var customer = new Customer("Customer 1", "customer1@email.com", "(11)90000-0001", "Address 1");
            var product = new Product("Product X", "Description", 10m, 100);

            var command = new CreateSaleCommand
            {
                SaleNumber = "S1",
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                PaymentMethod = "Credit Card",
                CustomerId = customer.Id,
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = product.Id,
                        Quantity = 2,
                        DiscountPercentage = 0
                    }
                }
            };

            var sale = new Sale(command.SaleNumber, command.SaleDate, customer, command.Branch, command.PaymentMethod);
            sale.AddItem(product.Id, product.Name, 2, product.Price, 0);

            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            _productRepositoryMock
                .Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _saleRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);

            _mapperMock
                .Setup(m => m.Map<CreateSaleCommandResult>(It.IsAny<Sale>()))
                .Returns(new CreateSaleCommandResult { Id = sale.Id });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
            _publishMock.Verify(p => p.Publish(It.IsAny<SaleCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
