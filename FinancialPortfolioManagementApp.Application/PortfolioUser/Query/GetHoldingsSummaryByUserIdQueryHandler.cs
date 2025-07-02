using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Application.Contracts;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Query
{
    public class GetHoldingsSummaryByUserIdQueryHandler : IRequestHandler<GetHoldingsSummaryByUserIdQuery, Result<PortfolioSummary>>
    {
        private readonly IAssetRepository _assetRepository;

        private readonly IHoldingRepository _holdingRepository;
        public GetHoldingsSummaryByUserIdQueryHandler(IAssetRepository assetRepository, IHoldingRepository holdingRepository)
        {
            _holdingRepository = holdingRepository;
            _assetRepository = assetRepository;
        }
        public async Task<Result<PortfolioSummary>> Handle(
                GetHoldingsSummaryByUserIdQuery request,
                CancellationToken cancellationToken)
        {
            var holdings = _holdingRepository.GetByUserId(request.userId);

            decimal? totalValue = 0;

            foreach (var item in holdings)
            {
                totalValue = totalValue + item?.Asset?.CurrentMarketPrice * item?.Quantity;
            }

            PortfolioSummary portfolioSummary = new PortfolioSummary()
            {
                Holdings = holdings.ToList(),
                TotalValue = (decimal)totalValue
            };

            return Result.Success(portfolioSummary);
        }
    }
}
