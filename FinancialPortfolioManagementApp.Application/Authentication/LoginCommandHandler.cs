using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Shared.Interfaces;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
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
