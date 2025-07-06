namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models
{
    public class HoldingsSummary
    {
        public List<HoldingDto> Holdings { get; set; } = new List<HoldingDto>();

        public decimal TotalValue { get; set; }
    }
}
