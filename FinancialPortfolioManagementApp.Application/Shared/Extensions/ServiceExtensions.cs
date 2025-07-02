using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FinancialPortfolioManagementApp.Application.Shared.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
