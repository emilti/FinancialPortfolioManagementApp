namespace FinancialPortfolioManagementApp.Api.Models.Assets.Requests
{
    public class CreateAssetRequest
    {
        public string Name { get; set; }
        public decimal CurrentMarketPrice { get; set; }
    }
}
