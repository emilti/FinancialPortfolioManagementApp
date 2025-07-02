using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;

namespace FinancialPortfolioManagementApp.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        public AssetRepository(FinancialPortfolioManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Asset Get(Guid id)
        {
            return _dbContext.Assets.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Asset asset)
        {
            _dbContext.Add(asset);
        }

        public void Update(Asset asset)
        {
            _dbContext.Update(asset);
        }

        public void Delete(Asset id)
        {
            _dbContext.Remove(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
