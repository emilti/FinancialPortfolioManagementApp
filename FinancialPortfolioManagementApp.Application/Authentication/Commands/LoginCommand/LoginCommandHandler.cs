using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthenticationResult>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<Result<AuthenticationResult>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {

            return await _authService.LoginAsync(
                request.Email,
                request.Password
               );
        }
    }
}
