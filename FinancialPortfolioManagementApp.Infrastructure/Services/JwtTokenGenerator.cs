using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialPortfolioManagementApp.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IUserMapper _userMapper;
        private readonly IConfiguration _configuration; 

        public JwtTokenGenerator(
            UserManager<AuthUser> userManager,
            IUserMapper userMapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userMapper = userMapper;
            _configuration= configuration;
        }

        public string GenerateToken(AuthUser user)
        {
            var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
