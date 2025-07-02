using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;

namespace FinancialPortfolioManagementApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        public UserRepository(FinancialPortfolioManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Add(user);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
