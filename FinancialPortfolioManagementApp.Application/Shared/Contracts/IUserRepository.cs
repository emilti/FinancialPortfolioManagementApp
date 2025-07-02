using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface IUserRepository
    {
        void Add(User user);

        void Save();
    }
}
