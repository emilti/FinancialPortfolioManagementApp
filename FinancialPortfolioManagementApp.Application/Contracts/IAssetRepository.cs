using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Contracts
{
    public interface IAssetRepository
    {
        public Asset Get(Guid id);

        void Add(Asset asset);

        void Update(Asset asset);

        void Delete(Asset asset);

        Task SaveAsync();
    }
}
