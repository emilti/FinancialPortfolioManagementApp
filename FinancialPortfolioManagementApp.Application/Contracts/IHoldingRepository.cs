using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Contracts
{
    public interface IHoldingRepository
    {
        public Holding Get(Guid UserId, Guid AssetId);

        void Add(Holding holding);

        void Update(Holding holding);

        void Delete(Holding holding);

        Task SaveAsync();
    }
}
