using FinancialPortfolioManagementApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Identity
{
    public class AuthUser : IdentityUser
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Guid? DomainUserId { get; set; }

        public virtual User? DomainUser { get; set; }
    }
}
