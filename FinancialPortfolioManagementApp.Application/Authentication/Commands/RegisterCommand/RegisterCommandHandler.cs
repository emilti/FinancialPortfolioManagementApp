using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication.Commands.RegisterCommand
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
