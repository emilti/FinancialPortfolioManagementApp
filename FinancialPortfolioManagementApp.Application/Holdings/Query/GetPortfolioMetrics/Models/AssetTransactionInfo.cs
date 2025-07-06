using FinancialPortfolioManagementApp.Domain.Enums;

namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models
{
    public class AssetTransactionInfo
    {
        public AssetTransactionInfo(decimal quantity, decimal priceAtTransaction, TransactionType type)
        {
            Quantity = quantity;
            PriceAtTransaction = priceAtTransaction;
            Type = type;
        }

        public decimal Quantity { get; set; }

        public decimal PriceAtTransaction { get; set; }

        public TransactionType Type { get; set; }

    }
}
