namespace FinancialPortfolioManagementApp.Api.Models.Assets.Requests
{
    public class BuyAssetRequest
    {
        public Guid UserId { get; set; }

        public decimal Quantity { get; set; }
    }
}
