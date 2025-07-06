using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Queries.GetAssetById
{
    public record GetAssetByIdQuery(Guid Id) : IRequest<Result<Asset>>;
}
