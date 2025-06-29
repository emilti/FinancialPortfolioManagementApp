using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Contracts
{
    public interface IUserMapper
    {
        public AuthUser ToAuthUser(User domainUser);

        public User ToDomainUser(AuthUser authUser);
        
    }
}
