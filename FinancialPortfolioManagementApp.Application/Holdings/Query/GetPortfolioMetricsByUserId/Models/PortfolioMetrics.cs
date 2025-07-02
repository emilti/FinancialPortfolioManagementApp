namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models
{
    public class PortfolioMetrics
    {
        public decimal TotalInvestedAmount { get; set; }

        public List<AssetInfo> CurrentMarketValues { get; set; }

        public decimal ReturnOnInvestment { get; set; }
    }
}
