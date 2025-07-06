using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;

namespace FinancialPortfolioManagementApp.Api.Models.Assets.Response
{
    public class PortfolioMetricsResponse
    {
        public decimal TotalInvestedAmount { get; set; }

        public List<AssetInfo> CurrentMarketValues { get; set; }

        public decimal ReturnOnInvestment { get; set; }
    }
}
