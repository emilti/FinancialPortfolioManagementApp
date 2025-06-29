namespace FinancialPortfolioManagementApp.Api.Models.Requests
{
    public class RegisterRequests
    {
        public string Email { get; init; }
        public string Password { get; init; }

        public RegisterRequests(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
