using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<AuthenticationResult>;
}
