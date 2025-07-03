using FinancialPortfolioManagementApp.Application.Authentication;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface IAuthService
    {
        Task<Result<AuthenticationResult>> RegisterAsync(User user, string password);
        Task<Result<AuthenticationResult>> LoginAsync(string email, string password);
    }
}
