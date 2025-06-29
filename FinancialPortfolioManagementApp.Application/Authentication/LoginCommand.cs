using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public record LoginCommand(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
}
