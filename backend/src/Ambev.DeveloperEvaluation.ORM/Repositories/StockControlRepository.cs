using Ambev.DeveloperEvaluation.Domain.Repositories;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class StockControlRepository : IStockControlRepository
    {
        private readonly DefaultContext _context;

        public StockControlRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<StockControl?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.StockControl.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<StockControl>> GetAllByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.StockControl
                .Where(s => s.ProductId == productId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<StockControl> CreateAsync(StockControl stockControl, CancellationToken cancellationToken = default)
        {
            await _context.StockControl.AddAsync(stockControl, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return stockControl;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var stock = await GetByIdAsync(id, cancellationToken);
            if (stock == null) return false;

            _context.StockControl.Remove(stock);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
