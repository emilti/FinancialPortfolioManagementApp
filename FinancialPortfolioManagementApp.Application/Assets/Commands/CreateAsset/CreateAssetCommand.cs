using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.CreateAsset
{
    public record CreateAssetCommand(
        string Name,
        decimal CurrentMarketPrice) : IRequest<Result<Asset>>;
}
