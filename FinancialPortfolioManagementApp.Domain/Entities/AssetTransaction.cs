using FinancialPortfolioManagementApp.Domain.Enums;

namespace FinancialPortfolioManagementApp.Domain.Entities
{
    public class AssetTransaction
    {
        public Guid Id { get; set; }

        public decimal Quantity { get; set; }

        public decimal PriceAtTransaction { get; set; }

        public TransactionType Type { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public required Guid AssetId { get; set; }

        public required Asset Asset { get; set; }

        public required Guid UserId { get; set; }

        public required User User { get; set; }
    }
}
