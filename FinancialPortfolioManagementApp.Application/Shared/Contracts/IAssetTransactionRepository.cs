using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface IAssetTransactionRepository
    {
        void Add(AssetTransaction assetTransaction);

        Task<IEnumerable<AssetTransaction>> GetByUserIdAsync(Guid userId);

        Task SaveAsync();
    }
}
