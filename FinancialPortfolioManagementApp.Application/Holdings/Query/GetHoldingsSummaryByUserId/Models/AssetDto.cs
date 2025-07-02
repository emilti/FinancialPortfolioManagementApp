namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models
{
    public class AssetDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal CurrentMarketPrice { get; set; }
    }
}
