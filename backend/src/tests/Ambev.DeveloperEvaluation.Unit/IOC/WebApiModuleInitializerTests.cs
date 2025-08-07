using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
public class WebApiModuleInitializerTests
{
    [Fact(DisplayName = "Deve registrar Controllers e HealthChecks")]
    public void Initialize_Should_RegisterControllersAndHealthChecks()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var initializer = new WebApiModuleInitializer();

        // Act
        initializer.Initialize(builder);
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var controllers = services.GetService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider>();
        var healthCheckService = services.GetService<Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckService>();

        Assert.NotNull(controllers);
        Assert.NotNull(healthCheckService);
    }
}
