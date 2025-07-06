using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Holdings.Query
{
    public record GetPortfolioMetricsQuery() : IRequest<Result<PortfolioMetrics>>;
}
