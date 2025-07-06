using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Queries.GetAllAssets
{
    public record GetAllAssetsQuery() : IRequest<Result<List<Asset>>>;
}
