using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;

namespace FinancialPortfolioManagementApp.Api.Models.Assets.Response
{
    public class HoldingsSummaryResponse
    {
        public List<HoldingResponse> Holdings { get; set; } = new List<HoldingResponse>();

        public decimal TotalValue { get; set; }
    }
}
