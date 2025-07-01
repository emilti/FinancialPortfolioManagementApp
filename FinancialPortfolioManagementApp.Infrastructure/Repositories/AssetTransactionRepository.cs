using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;

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

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
