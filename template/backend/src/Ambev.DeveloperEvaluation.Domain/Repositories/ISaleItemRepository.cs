using Ambev.DeveloperEvaluation.Domain.Entities;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SaleItem> CreateAsync(SaleItem sale, CancellationToken cancellationToken = default);
    Task UpdateAsync(SaleItem sale, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
