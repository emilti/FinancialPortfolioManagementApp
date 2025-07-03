using AutoMapper;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using FinancialPortfolioManagementApp.Infrastructure.Repositories;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IHoldingRepository, HoldingRepository>();
            services.AddScoped<IAssetTransactionRepository, AssetTransactionRepository>();
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
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
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context => {
                        Console.WriteLine("Token validated successfully!");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
