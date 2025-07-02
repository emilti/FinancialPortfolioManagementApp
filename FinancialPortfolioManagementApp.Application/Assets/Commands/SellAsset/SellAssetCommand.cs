using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset
{
    public record SellAssetCommand(Guid UserId, Guid AssetId, decimal Quantity) : IRequest<Result<bool>>;
}
