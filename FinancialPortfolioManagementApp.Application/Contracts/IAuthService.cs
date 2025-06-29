using FinancialPortfolioManagementApp.Application.Authentication;
using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthenticationResult>> RegisterAsync(User user, string password);
        //Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
