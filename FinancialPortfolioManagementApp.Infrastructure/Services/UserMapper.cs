using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Services
{
    public class UserMapper : IUserMapper
    {
        public AuthUser ToAuthUser(User domainUser)
        {
            return new AuthUser
            {
                UserName = domainUser.Email,
            };
        }

        public User ToDomainUser(AuthUser authUser)
        {
            return new User(authUser.Email);
        }
    }
}
