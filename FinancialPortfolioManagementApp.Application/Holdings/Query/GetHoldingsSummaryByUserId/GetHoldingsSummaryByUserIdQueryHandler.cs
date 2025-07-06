using AutoMapper;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId
{
    public class GetHoldingsSummaryByUserIdQueryHandler : IRequestHandler<GetHoldingsSummaryByUserIdQuery, Result<HoldingsSummary>>
    {
        private readonly IHoldingRepository _holdingRepository;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;
        public GetHoldingsSummaryByUserIdQueryHandler(
            IHoldingRepository holdingRepository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _holdingRepository = holdingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<Result<HoldingsSummary>> Handle(
                GetHoldingsSummaryByUserIdQuery request,
                CancellationToken cancellationToken)
        {
            Guid userId = new Guid(_currentUserService.UserId);
            var holdings = await _holdingRepository.GetByUserIdAsync(userId);
            decimal? totalValue = GetTotalValue(holdings);

            var resultHoldings = _mapper.Map<List<HoldingDto>>(holdings.ToList());

            HoldingsSummary portfolioSummary = new HoldingsSummary()
            {
                Holdings = resultHoldings,
                TotalValue = (decimal)totalValue
            };

            return Result.Success(portfolioSummary);
        }

        private static decimal? GetTotalValue(IEnumerable<Holding> holdings)
        {
            decimal? totalValue = 0;

            foreach (var item in holdings)
            {
                totalValue = totalValue + item?.Asset?.CurrentMarketPrice * item?.Quantity;
            }

            return totalValue;
        }
    }
}
