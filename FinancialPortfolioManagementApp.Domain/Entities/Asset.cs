namespace FinancialPortfolioManagementApp.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }

        public required string Name  { get; set; }

        public Decimal CurrentMarketPrice { get; set; }

        public ICollection<AssetTransaction> Transactions { get; set; } = new List<AssetTransaction>();

        public ICollection<Holding> Holdings { get; set; } = new List<Holding>();
    }
}
