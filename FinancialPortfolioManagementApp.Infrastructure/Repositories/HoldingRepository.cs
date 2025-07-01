using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;

namespace FinancialPortfolioManagementApp.Infrastructure.Repositories
{
    public class HoldingRepository : IHoldingRepository
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        public HoldingRepository(FinancialPortfolioManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Holding Get(Guid userId, Guid assetId)
        {
            return _dbContext.Holdings.FirstOrDefault(x => x.UserId == userId && x.AssetId == assetId);
        }

        public void Add(Holding holding)
        {
            _dbContext.Add(holding);
        }

        public void Update(Holding holding)
        {
            _dbContext.Update(holding);
        }

        public void Delete(Holding id)
        {
            _dbContext.Remove(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
