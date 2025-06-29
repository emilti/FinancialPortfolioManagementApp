using FinancialPortfolioManagementApp.Application.Authentication;
using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Domain.Interfaces;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;
        private readonly IUserMapper _mapper;

        public AuthService(
            UserManager<AuthUser> userManager,
            IJwtTokenGenerator tokenGenerator,
            FinancialPortfolioManagementAppDbContext dbContext,
            IUserMapper mapper)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<AuthenticationResult>> RegisterAsync(
            User user,
            string password
            )
        {
            var authUser = _mapper.ToAuthUser(user);

            var result = await _userManager.CreateAsync(authUser, password);
            if (!result.Succeeded)
            {
                return Result.Failure<AuthenticationResult>(result.Errors.Select(x => x.Description));
            }

            var token = _tokenGenerator.GenerateToken(authUser);
            
            return new AuthenticationResult(user, token);
        }
    }
}
