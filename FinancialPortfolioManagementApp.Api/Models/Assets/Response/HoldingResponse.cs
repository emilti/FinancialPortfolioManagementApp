using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;

namespace FinancialPortfolioManagementApp.Api.Models.Assets.Response
{
    public class HoldingResponse
    {
        public Guid Id { get; set; }

        public decimal Quantity { get; set; }

        public required Guid AssetId { get; set; }

        public AssetDto? Asset { get; set; }

        public required Guid UserId { get; set; }

        public required string UserEmail { get; set; }
    }
}
