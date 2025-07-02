using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId
{
    public record GetHoldingsSummaryByUserIdQuery(Guid userId) : IRequest<Result<HoldingsSummary>>;
}
