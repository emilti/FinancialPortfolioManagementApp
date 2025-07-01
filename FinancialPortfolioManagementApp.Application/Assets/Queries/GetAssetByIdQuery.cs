using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Queries
{
    public record GetAssetByIdQuery(Guid Id) : IRequest<Result<Asset>>;
}
