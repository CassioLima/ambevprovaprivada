using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Cache
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
        Task SetAsync<T>(string key, T data, TimeSpan expiration, CancellationToken cancellationToken = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
