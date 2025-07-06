using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface IAssetRepository
    {
        Task<Asset?> GetAsync(Guid id);

        Task<List<Asset>> GetAllAsync();

        void Add(Asset asset);

        void Update(Asset asset);

        void Delete(Asset asset);

        Task SaveAsync();
    }
}
