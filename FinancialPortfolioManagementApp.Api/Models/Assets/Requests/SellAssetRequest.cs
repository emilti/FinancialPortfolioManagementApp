namespace FinancialPortfolioManagementApp.Api.Models.Assets.Requests
{
    public class SellAssetRequest
    {
        public Guid UserId { get; set; }

        public decimal Quantity { get; set; }
    }
}
