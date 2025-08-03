using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
public class ApplicationModuleInitializerTests
{
    [Fact(DisplayName = "Deve registrar BCryptPasswordHasher como IPasswordHasher")]
    public void Initialize_Should_RegisterPasswordHasher()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var initializer = new ApplicationModuleInitializer();

        // Act
        initializer.Initialize(builder);
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var passwordHasher = services.GetService<IPasswordHasher>();
        Assert.NotNull(passwordHasher);
        Assert.IsType<BCryptPasswordHasher>(passwordHasher);
    }
}
