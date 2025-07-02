namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models
{
    public class AssetInfo
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }
    }
}
