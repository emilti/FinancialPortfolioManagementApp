using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Api.Models.Assets.Response
{
    public class AssetResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal CurrentMarketPrice { get; set; }
    }
}
