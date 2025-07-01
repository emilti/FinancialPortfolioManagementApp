namespace FinancialPortfolioManagementApp.Api.Models.Requests
{
    public class UpdateAssetRequest
    {
        public string Name { get; init; }
        public Decimal CurrentMarketPrice { get; init; }

        public UpdateAssetRequest(string name, Decimal currentMarketPrice)
        {
            Name = name;
            CurrentMarketPrice = currentMarketPrice;
        }
    }
}
