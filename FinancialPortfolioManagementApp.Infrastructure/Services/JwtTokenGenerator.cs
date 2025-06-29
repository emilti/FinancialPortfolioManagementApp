using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialPortfolioManagementApp.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
      //  private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IUserMapper _userMapper;

        public JwtTokenGenerator(
           // IOptions<JwtSettings> jwtSettings,
            UserManager<AuthUser> userManager,
            IUserMapper userMapper)
        {
            //_jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _userMapper = userMapper;
        }

        public string GenerateToken(AuthUser user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test")),//)_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "issuer",// _jwtSettings.Issuer,
                audience: "MenuCreated", //_jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),//);_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
