using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication.Commands.LoginCommand
{
    public record LoginCommand(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;
}
