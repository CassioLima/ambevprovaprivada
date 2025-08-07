using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.Threading.Tasks;
using System;
using Ambev.DeveloperEvaluation.WebApi.Middleware;

namespace Ambev.DeveloperEvaluation.Tests.Infrastructure.Middleware
{
    public class ExceptionMiddlewareTests
    {
        [Fact(DisplayName = "Invocar middleware não deve lançar exceção")]
        public async Task Invoke_Should_NotThrow()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            var logger = new Mock<ILogger<ExceptionMiddleware>>().Object;
            var env = new Mock<IWebHostEnvironment>().Object;

            var middleware = new ExceptionMiddleware(next, logger, env);
            var context = new DefaultHttpContext();

            // Act & Assert
            await middleware.InvokeAsync(context);
        }
    }
}
