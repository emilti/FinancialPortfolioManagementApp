using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Contracts
{
    public interface IAssetTransactionRepository
    {
        void Add(AssetTransaction assetTransaction);

        Task SaveAsync();
    }
}
