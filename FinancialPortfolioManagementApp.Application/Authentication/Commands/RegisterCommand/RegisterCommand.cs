using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication.Commands.RegisterCommand
{
    public record RegisterCommand(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;
}
