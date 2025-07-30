using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Tests.Common
{
    public class HealthChecksExtensionTests
    {
        [Fact(DisplayName = "AddHealthChecks não deve lançar exceção")]
        public void AddHealthChecks_Should_NotThrow()
        {
            var builder = WebApplication.CreateBuilder();

            // Aqui usamos o padrão direto do framework
            builder.Services.AddHealthChecks();

            var app = builder.Build();
            Assert.NotNull(app);
        }
    }
}
