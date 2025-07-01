using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Contracts
{
    public interface IUserRepository
    {
        void Add(User user);

        void Save();
    }
}
