using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.UpdateAsset
{
    public record UpdateAssetCommand(
       Guid Id,
       string Name,
       decimal CurrentMarketPrice) : IRequest<Result<bool>>;
}
