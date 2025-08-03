using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Cache;

namespace Ambev.DeveloperEvaluation.Tests.IoC
{
    public class InfrastructureModuleInitializerTests
    {
        [Fact(DisplayName = "Deve registrar repositórios e RedisCacheService")]
        public void Initialize_Should_RegisterRepositoriesAndCache()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddDbContext<Ambev.DeveloperEvaluation.ORM.DefaultContext>();
            builder.Services.AddDistributedMemoryCache(); 

            var initializer = new InfrastructureModuleInitializer();

            // Act
            initializer.Initialize(builder);
            var services = builder.Services.BuildServiceProvider();

            // Assert
            Assert.NotNull(services.GetService<IUserRepository>());
            Assert.NotNull(services.GetService<ISaleRepository>());
            Assert.NotNull(services.GetService<ISaleItemRepository>());
            Assert.NotNull(services.GetService<ICustomerRepository>());
            Assert.NotNull(services.GetService<IProductRepository>());
            Assert.NotNull(services.GetService<IStockControlRepository>());
            Assert.NotNull(services.GetService<IRedisCacheService>());
        }
    }
}
