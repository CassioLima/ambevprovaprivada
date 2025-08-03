using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Infrastructure.Cache
{
    public class RedisCacheServiceTests
    {
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly IRedisCacheService _redisCacheService;

        public RedisCacheServiceTests()
        {
            _cacheMock = new Mock<IDistributedCache>();
            _redisCacheService = new RedisCacheService(_cacheMock.Object);
        }

        [Fact(DisplayName = "GetAsync deve retornar objeto quando existir no cache")]
        public async Task GetAsync_Should_Return_Object_When_Cached()
        {
            // Arrange
            var key = "test-key";
            var expectedObject = new TestData { Id = 1, Name = "Redis Test" };
            var serialized = JsonSerializer.SerializeToUtf8Bytes(expectedObject);

            _cacheMock.Setup(c => c.GetAsync(key, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(serialized);

            // Act
            var result = await _redisCacheService.GetAsync<TestData>(key);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedObject.Id, result!.Id);
            Assert.Equal(expectedObject.Name, result.Name);
        }

        [Fact(DisplayName = "GetAsync deve retornar null quando chave não existe no cache")]
        public async Task GetAsync_Should_Return_Null_When_Not_Cached()
        {
            // Arrange
            var key = "missing-key";
            _cacheMock.Setup(c => c.GetAsync(key, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((byte[]?)null);

            // Act
            var result = await _redisCacheService.GetAsync<TestData>(key);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "SetAsync deve salvar objeto no cache")]
        public async Task SetAsync_Should_Save_Object_To_Cache()
        {
            // Arrange
            var key = "save-key";
            var data = new TestData { Id = 2, Name = "Save Test" };
            var expiration = TimeSpan.FromMinutes(1);

            _cacheMock.Setup(c => c.SetAsync(
                    key,
                    It.IsAny<byte[]>(),
                    It.IsAny<DistributedCacheEntryOptions>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _redisCacheService.SetAsync(key, data, expiration);

            // Assert
            _cacheMock.Verify();
        }

        [Fact(DisplayName = "RemoveAsync deve remover objeto do cache")]
        public async Task RemoveAsync_Should_Remove_Object_From_Cache()
        {
            // Arrange
            var key = "remove-key";
            _cacheMock.Setup(c => c.RemoveAsync(key, It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask)
                      .Verifiable();

            // Act
            await _redisCacheService.RemoveAsync(key);

            // Assert
            _cacheMock.Verify();
        }

        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}
