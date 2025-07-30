using Xunit;
using Moq;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;

namespace Ambev.DeveloperEvaluation.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new ProductsController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "GetAllProducts deve retornar OkResult")]
        public async Task GetAllProducts_Should_Return_Ok()
        {
            var result = await _controller.GetAllProducts(CancellationToken.None);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
