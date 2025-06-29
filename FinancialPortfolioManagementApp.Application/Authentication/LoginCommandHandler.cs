using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
