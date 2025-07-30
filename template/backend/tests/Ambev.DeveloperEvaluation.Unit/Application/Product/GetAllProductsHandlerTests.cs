using Xunit;
using Moq;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;

public class GetAllProductsHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRedisCacheService> _cacheMock;
    private readonly GetAllProductsHandler _handler;

    public GetAllProductsHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _cacheMock = new Mock<IRedisCacheService>();
        _handler = new GetAllProductsHandler(_productRepositoryMock.Object, _mapperMock.Object, _cacheMock.Object);
    }

    [Fact(DisplayName = "Deve retornar do cache quando existir")]
    public async Task Handle_Should_Return_From_Cache_When_Exists()
    {
        var cachedData = new List<GetAllProductsResult> { new GetAllProductsResult { Id = System.Guid.NewGuid(), Name = "Cached Product" } };
        _cacheMock.Setup(c => c.GetAsync<List<GetAllProductsResult>>("products:all", It.IsAny<CancellationToken>()))
                  .ReturnsAsync(cachedData);

        var result = await _handler.Handle(new GetAllProductsCommand(), CancellationToken.None);

        Assert.Single(result);
        Assert.Equal("Cached Product", result[0].Name);
    }

    [Fact(DisplayName = "Deve buscar no repositório e salvar no cache quando não existir no cache")]
    public async Task Handle_Should_Fetch_And_Cache_When_Not_Cached()
    {
        _cacheMock.Setup(c => c.GetAsync<List<GetAllProductsResult>>("products:all", It.IsAny<CancellationToken>()))
                  .ReturnsAsync((List<GetAllProductsResult>?)null);

        var products = new List<Product> { new Product("Product Repo", "Desc", 10, 1) };
        _productRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(products);

        var mapped = new List<GetAllProductsResult> { new GetAllProductsResult { Id = products[0].Id, Name = products[0].Name } };
        _mapperMock.Setup(m => m.Map<List<GetAllProductsResult>>(products)).Returns(mapped);

        var result = await _handler.Handle(new GetAllProductsCommand(), CancellationToken.None);

        Assert.Single(result);
        _cacheMock.Verify(c => c.SetAsync("products:all", mapped, It.IsAny<System.TimeSpan>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
