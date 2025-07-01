namespace FinancialPortfolioManagementApp.Api.Models.Requests
{
    public class CreateAssetRequest
    {
        public string Name { get; set; }
        public decimal CurrentMarketPrice { get; set; }
    }
}
