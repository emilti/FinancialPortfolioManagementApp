using FinancialPortfolioManagementApp.Infrastructure.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AuthUser user);
    }
}
