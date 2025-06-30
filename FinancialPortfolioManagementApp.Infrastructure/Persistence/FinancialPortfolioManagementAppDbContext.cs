using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagementApp.Infrastructure.Persistence
{
    public class FinancialPortfolioManagementAppDbContext : IdentityDbContext<AuthUser, Role, Guid>
    {
        public DbSet<User> PortfolioUsers { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        public DbSet<Holding> Holdings { get; set; }

        public FinancialPortfolioManagementAppDbContext(DbContextOptions<FinancialPortfolioManagementAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuthUser>(authUser =>
            {
                authUser.ToTable("Users");
                authUser.Property(u => u.CreatedAt)
                       .HasDefaultValueSql("GETUTCDATE()");

                authUser.HasOne(a => a.PortfolioUser)
                   .WithOne()
                   .HasForeignKey<User>(u => u.Id) 
                   .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.HasMany(u => u.AssetTransactions)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Holdings)
                      .WithOne(h => h.User)
                      .HasForeignKey(h => h.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Asset>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired();
                entity.Property(a => a.CurrentMarketPrice)
                    .HasPrecision(18, 6);
                entity.HasMany(a => a.Transactions)
                      .WithOne(t => t.Asset)
                      .HasForeignKey(t => t.AssetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(a => a.Holdings)
                      .WithOne(h => h.Asset)
                      .HasForeignKey(h => h.AssetId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AssetTransaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Quantity)
                      .HasPrecision(18, 8);

                entity.Property(t => t.PriceAtTransaction)
                      .HasPrecision(18, 2);

                entity.HasIndex(t => new { t.UserId, t.AssetId, t.Timestamp });
            });

            builder.Entity<Holding>(entity =>
            {
                entity.HasKey(h => h.Id);

                entity.Property(h => h.Quantity)
                      .HasPrecision(18, 8);

                entity.HasIndex(h => new { h.UserId, h.AssetId }).IsUnique();
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
