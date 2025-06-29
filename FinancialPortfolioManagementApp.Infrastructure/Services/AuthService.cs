using FinancialPortfolioManagementApp.Application.Authentication;
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

        public async Task<AuthenticationResult> RegisterAsync(
            User user,
            string password
            )
        {
            var authUser = _mapper.ToAuthUser(user);

            // Transaction ensures both succeed or fail together
            //using var transaction = await _dbContext.Database.BeginTransactionAsync();

            //try
            //{
                _dbContext.Users.Add(authUser);
                await _dbContext.SaveChangesAsync();

                var result = await _userManager.CreateAsync(authUser, password);
                //if (!result.Succeeded)
                //{
                //    await transaction.RollbackAsync();
                //}

                //await transaction.CommitAsync();

                // Generate tokens
                var token = _tokenGenerator.GenerateToken(authUser);
                //var refreshToken = await _userManager.GenerateUserTokenAsync(
                //    authUser,
                //    TokenOptions.DefaultProvider,
                //    "Refresh");

                return new AuthenticationResult(user, token);
            //}
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //    throw new Exception();
            //    //return Result.Fail(ex.Message);
            //}
        }
    }
}
