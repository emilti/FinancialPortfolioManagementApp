using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagementApp.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        public AssetRepository(FinancialPortfolioManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Asset?> GetAsync(Guid id)
        {
            return await _dbContext.Assets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Asset>> GetAllAsync()
        {
            return await _dbContext.Assets.ToListAsync();
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
