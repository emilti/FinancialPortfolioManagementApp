using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagementApp.Infrastructure.Repositories
{
    public class AssetTransactionRepository : IAssetTransactionRepository
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        public AssetTransactionRepository(FinancialPortfolioManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(AssetTransaction assetTransaction)
        {
            _dbContext.Add(assetTransaction);
        }

        public async Task<IEnumerable<AssetTransaction>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.AssetTransactions
                .Where(x => x.UserId == userId)
                .Include(x => x.Asset)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
