namespace FinancialPortfolioManagementApp.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(FinancialPortfolioManagementAppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            await context.SeedAssetsAsync();
        }
    }
}
