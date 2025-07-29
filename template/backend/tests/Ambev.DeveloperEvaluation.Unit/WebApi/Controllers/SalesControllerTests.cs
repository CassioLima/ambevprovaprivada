using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Tests.Controllers
{
    public class SalesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new SalesController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "CreateSale deve retornar OkObjectResult quando a venda for criada com sucesso")]
        public async Task CreateSale_Should_ReturnOkObjectResult()
        {
            // Arrange
            var request = new CreateSaleRequest();
            var command = new CreateSaleCommand();
            var resultCommand = new CreateSaleCommandResult { Id = Guid.NewGuid() };
            var response = new CreateSaleResponse { Id = resultCommand.Id };

            _mapperMock.Setup(m => m.Map<CreateSaleCommand>(request)).Returns(command);
            _mapperMock.Setup(m => m.Map<CreateSaleResponse>(resultCommand)).Returns(response);
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(resultCommand);

            // Act
            var result = await _controller.CreateSale(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<CreatedResult>(result);
            var apiResponse = Assert.IsType<ApiResponseWithData<CreateSaleResponse>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(resultCommand.Id, apiResponse.Data.Id);
        }
    }
}
