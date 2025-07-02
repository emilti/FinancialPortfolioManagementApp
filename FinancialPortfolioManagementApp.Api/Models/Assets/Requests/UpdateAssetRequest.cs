namespace FinancialPortfolioManagementApp.Api.Models.Assets.Requests
{
    public class UpdateAssetRequest
    {
        public string Name { get; init; }
        public decimal CurrentMarketPrice { get; init; }

        public UpdateAssetRequest(string name, decimal currentMarketPrice)
        {
            Name = name;
            CurrentMarketPrice = currentMarketPrice;
        }
    }
}
