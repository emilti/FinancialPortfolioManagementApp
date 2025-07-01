using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Domain.Interfaces;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Result<AuthenticationResult>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var user = new User(request.Email);
            var result = await _authService.RegisterAsync(
                user,
                request.Password
               );

            user.Id = result.Data.User.Id;

            _userRepository.Add(user);
            _userRepository.Save();

            return result;
        }
    }
}
