namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
    }
}
