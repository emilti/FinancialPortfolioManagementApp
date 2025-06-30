using FinancialPortfolioManagementApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Identity
{
    public class AuthUser : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public virtual User? PortfolioUser { get; set; }
    }
}
