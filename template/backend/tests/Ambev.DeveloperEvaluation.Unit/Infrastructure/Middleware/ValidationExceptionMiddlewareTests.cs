using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using System.IO;

namespace Ambev.DeveloperEvaluation.Tests.Middlewares
{
    public class ValidationExceptionMiddlewareTests
    {
        [Fact(DisplayName = "InvokeAsync deve continuar sem falha quando não há exceção de validação")]
        public async Task InvokeAsync_Should_Continue_When_No_ValidationException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            RequestDelegate next = (HttpContext ctx) => Task.CompletedTask;
            var middleware = new ValidationExceptionMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        }
    }
}
