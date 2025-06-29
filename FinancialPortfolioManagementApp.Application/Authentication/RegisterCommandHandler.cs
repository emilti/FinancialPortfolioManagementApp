using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Domain.Interfaces;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<Result<AuthenticationResult>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var user = new User(request.Email);

            return await _authService.RegisterAsync(
                user,
                request.Password
               );
        }
    }
}
