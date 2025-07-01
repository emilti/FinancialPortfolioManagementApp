using FinancialPortfolioManagementApp.Application.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Commands
{
    public record BuyAssetCommand(Guid UserId, Guid AssetId, Decimal Quantity) : IRequest<Result<bool>>;
}
