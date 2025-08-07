using Xunit;
using Moq;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;

public class GetProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetProductHandler(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Deve retornar produto quando existir")]
    public async Task Handle_Should_Return_Product_When_Exists()
    {
        var id = Guid.NewGuid();
        var command = new GetProductCommand(id);
        var product = new Product("Product A", "Desc", 10, 1);
        var resultMapped = new GetProductResult { Id = product.Id, Name = product.Name };

        _productRepositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<GetProductResult>(product)).Returns(resultMapped);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }

    [Fact(DisplayName = "Deve lançar KeyNotFoundException quando produto não existir")]
    public async Task Handle_Should_Throw_KeyNotFound_When_Not_Exists()
    {
        var id = Guid.NewGuid();
        var command = new GetProductCommand(id);

        _productRepositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact(DisplayName = "Deve lançar ValidationException quando comando inválido")]
    public async Task Handle_Should_Throw_ValidationException_When_Invalid()
    {
        var command = new GetProductCommand(Guid.Empty); // inválido

        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
