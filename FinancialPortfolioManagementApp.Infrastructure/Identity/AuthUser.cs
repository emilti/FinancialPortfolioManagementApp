using Microsoft.AspNetCore.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Identity
{
    public class AuthUser : IdentityUser
    {
        public Guid Id { get; private set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
