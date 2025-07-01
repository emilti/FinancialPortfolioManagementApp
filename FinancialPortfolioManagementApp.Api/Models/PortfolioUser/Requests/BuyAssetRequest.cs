namespace FinancialPortfolioManagementApp.Api.Models.PortfolioUser.Requests
{
    public class BuyAssetRequest
    {
        public Guid UserId { get; set; }

        public Guid AsetId { get; set; }

        public Decimal Quantity { get; set; }
    }
}
