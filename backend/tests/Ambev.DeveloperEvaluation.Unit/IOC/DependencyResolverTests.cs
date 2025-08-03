using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.IoC;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class DependencyResolverTests
    {
        [Fact(DisplayName = "RegisterDependencies deve registrar serviços esperados no container")]
        public void RegisterDependencies_Should_Register_All_Services()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();

            // Adiciona um DbContext em memória para satisfazer os repositórios
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Adiciona implementação de IDistributedCache usando MemoryDistributedCache
            builder.Services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

            // Act
            builder.RegisterDependencies();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Assert - verifica alguns serviços chave
            Assert.NotNull(serviceProvider.GetService<IPasswordHasher>());
            Assert.NotNull(serviceProvider.GetService<IUserRepository>());
            Assert.NotNull(serviceProvider.GetService<IRedisCacheService>());
            var controllers = serviceProvider.GetService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider>();
            Assert.NotNull(controllers);
        }
    }
}
