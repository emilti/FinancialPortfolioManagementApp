using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using FinancialPortfolioManagementApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

        public async Task<IEnumerable<Holding>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Holdings.Where(x => x.UserId == userId).Include(x => x.Asset).ToListAsync();
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

        public async Task<ITransaction> BeginTransactionAsync()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            return new EfTransaction(transaction);
        }
    }
}
