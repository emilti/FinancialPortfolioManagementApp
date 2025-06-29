using FinancialPortfolioManagementApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagementApp.Infrastructure.Persistence
{
    public class FinancialPortfolioManagementAppDbContext : IdentityDbContext<IdentityUser>
        {
            public DbSet<AuthUser> Users { get; set; }

            public FinancialPortfolioManagementAppDbContext(DbContextOptions<FinancialPortfolioManagementAppDbContext> options)
                : base(options) { }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<AuthUser>(b =>
                {
                    b.HasKey(u => u.Id);
                    b.Property(u => u.Email).IsRequired();
                });
            }
        }
}
