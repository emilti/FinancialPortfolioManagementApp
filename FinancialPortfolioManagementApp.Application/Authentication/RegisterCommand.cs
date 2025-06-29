using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public record RegisterCommand(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
}
