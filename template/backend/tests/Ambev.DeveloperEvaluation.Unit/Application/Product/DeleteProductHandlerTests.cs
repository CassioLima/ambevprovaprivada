using Xunit;
using Moq;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DeleteProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new DeleteProductHandler(_productRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve deletar produto quando existir")]
    public async Task Handle_Should_Delete_Product_When_Exists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteProductCommand(id);

        _productRepositoryMock.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
    }

    [Fact(DisplayName = "Deve lançar KeyNotFoundException quando produto não existir")]
    public async Task Handle_Should_Throw_When_Product_NotFound()
    {
        var id = Guid.NewGuid();
        var command = new DeleteProductCommand(id);

        _productRepositoryMock.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact(DisplayName = "Deve lançar ValidationException quando comando inválido")]
    public async Task Handle_Should_Throw_ValidationException_When_Invalid()
    {
        var command = new DeleteProductCommand(Guid.Empty); // ID inválido

        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
