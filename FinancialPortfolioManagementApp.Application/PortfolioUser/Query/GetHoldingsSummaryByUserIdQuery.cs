using FinancialPortfolioManagementApp.Application.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Query
{
    public record GetHoldingsSummaryByUserIdQuery(Guid userId) : IRequest<Result<PortfolioSummary>>;
}
