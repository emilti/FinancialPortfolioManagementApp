using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Query
{
    public class PortfolioSummary
    {
        public List<Holding> Holdings { get; set; } = new List<Holding>();

        public Decimal TotalValue { get; set; }
    }
}
