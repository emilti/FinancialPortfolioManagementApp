using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.DeleteAsset
{
    public record DeleteAssetCommand(Guid id
        ) : IRequest<Result<bool>>;
}
