using Microsoft.AspNetCore.Identity;

namespace FinancialPortfolioManagementApp.Infrastructure.Identity
{
    public class AuthUser : IdentityUser<Guid>
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
