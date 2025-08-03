using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
