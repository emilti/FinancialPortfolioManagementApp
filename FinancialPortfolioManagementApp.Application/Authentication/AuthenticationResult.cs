using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public class AuthenticationResult
    {

        public User User { get; set; }

        public string Token { get; set; }

        public AuthenticationResult(User user, string token ) 
        { 
            User = user;
            Token = token;
        }
    }
}
