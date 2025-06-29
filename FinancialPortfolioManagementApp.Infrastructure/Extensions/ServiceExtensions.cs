using FinancialPortfolioManagementApp.Domain.Interfaces;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using FinancialPortfolioManagementApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinancialPortfolioManagementApp.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database
            services.AddDbContext<FinancialPortfolioManagementAppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("FinancialPortfolioManagementDb"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(FinancialPortfolioManagementAppDbContext).Assembly.FullName)));

            // Identity
            services.AddIdentityCore<AuthUser>()
                .AddEntityFrameworkStores<FinancialPortfolioManagementAppDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization();

            // JWT Authentication
            services.AddAuth(configuration);

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserMapper, UserMapper>();

            return services;
        }

        private static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.Configure<JwtSettings>(
            //    configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "tetsttsa", // configuration["JwtSettings:Issuer"],
                    ValidAudience = "tetsttsa",//configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("key"))//configuration["JwtSettings:SecretKey"]))
                };
            });

            return services;
        }
    }
}
