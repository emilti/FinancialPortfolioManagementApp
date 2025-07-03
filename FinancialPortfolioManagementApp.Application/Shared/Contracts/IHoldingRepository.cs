using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface IHoldingRepository
    {
        public Holding Get(Guid UserId, Guid AssetId);

        Task<IEnumerable<Holding>> GetByUserIdAsync(Guid userId);

        void Add(Holding holding);

        void Update(Holding holding);

        void Delete(Holding holding);

        Task SaveAsync();

        Task<ITransaction> BeginTransactionAsync();
    }
}
