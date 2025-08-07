using Xunit;
using Moq;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;

public class CreateProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateProductHandler(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Deve criar um produto válido")]
    public async Task Handle_Should_Create_Product_When_Valid()
    {
        var command = new CreateProductCommand { Name = "Product A", Price = 10, StockQuantity = 5 };
        var product = new Product("Product A", "Description", 10, 5);
        var result = new CreateProductResult { Id = product.Id };

        _mapperMock.Setup(m => m.Map<Product>(command)).Returns(product);
        _productRepositoryMock.Setup(r => r.CreateAsync(product, It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<CreateProductResult>(product)).Returns(result);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(product.Id, response.Id);
    }

    [Fact(DisplayName = "Deve lançar ValidationException quando dados são inválidos")]
    public async Task Handle_Should_Throw_ValidationException_When_Invalid()
    {
        var command = new CreateProductCommand(); // inválido (sem nome e preço zero)

        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
