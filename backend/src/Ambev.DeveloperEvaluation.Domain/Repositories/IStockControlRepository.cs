using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IStockControlRepository
    {
        Task<StockControl?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockControl>> GetAllByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<StockControl> CreateAsync(StockControl stockControl, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
