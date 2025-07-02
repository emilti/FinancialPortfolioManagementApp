using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Query
{
    public record GetPortfolioMetricsByUserIdQuery(Guid userId) : IRequest<Result<PortfolioMetrics>>;
}
