using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagementApp.Infrastructure.Persistence
{
    public class FinancialPortfolioManagementAppDbContext : IdentityDbContext<AuthUser>
    {
        public DbSet<AuthUser> Users { get; set; }

        public FinancialPortfolioManagementAppDbContext(DbContextOptions<FinancialPortfolioManagementAppDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AuthUser>(b =>
            {
                b.ToTable("Users");
            });

            builder.Ignore<IdentityRole>();
            builder.Ignore<IdentityUserRole<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserClaim<string>>();
        }
    }
}
