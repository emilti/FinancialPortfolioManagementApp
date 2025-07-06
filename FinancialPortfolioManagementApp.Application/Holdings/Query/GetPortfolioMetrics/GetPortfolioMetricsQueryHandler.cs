using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using FinancialPortfolioManagementApp.Domain.Enums;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Holdings.Query
{
    public class GetPortfolioMetricsQueryHandler : IRequestHandler<GetPortfolioMetricsQuery, Result<PortfolioMetrics>>
    {
        private readonly IHoldingRepository _holdingRepository;

        private readonly IAssetTransactionRepository _assetTransactionRepository;

        private readonly ICurrentUserService _currentUserService;
        public GetPortfolioMetricsQueryHandler(
            IHoldingRepository holdingRepository,
            IAssetTransactionRepository assetTransactionRepository,
            ICurrentUserService currentUserService)
        {
            _holdingRepository = holdingRepository;
            _assetTransactionRepository = assetTransactionRepository;
            _currentUserService = currentUserService;
        }
        public async Task<Result<PortfolioMetrics>> Handle(
                GetPortfolioMetricsQuery request,
                CancellationToken cancellationToken)
        {
            Guid userId = new Guid(_currentUserService.UserId);
            var userAssetTransactions = await _assetTransactionRepository
               .GetByUserIdAsync(userId);
            var userAssetTransactionInfos = userAssetTransactions
                .Select(x => new AssetTransactionInfo(
                    x.Quantity,
                    x.PriceAtTransaction,
                    x.Type))
                .ToList();
            decimal totalInvestedAmount = GetTotalInvestedAmoutAsync(userAssetTransactions);
            var holdings = await _holdingRepository.GetByUserIdAsync(userId);
            List<AssetInfo> assetInfos = GetCurrentMarketValues(holdings);
            decimal currentTotalValue = GetCurrentTotalValue(holdings);
            decimal returnOnInvestment = 0;
            if (totalInvestedAmount > 0)
            {
                returnOnInvestment = ((currentTotalValue - totalInvestedAmount) * 100) / totalInvestedAmount;
            }

            PortfolioMetrics portfolioMetrics = new PortfolioMetrics()
            {
                TotalInvestedAmount = totalInvestedAmount,
                CurrentMarketValues = assetInfos,
                ReturnOnInvestment = returnOnInvestment
            };

            return Result.Success(portfolioMetrics);
        }

        private static List<AssetInfo> GetCurrentMarketValues(IEnumerable<Holding> holdings)
        {
            var assetInfos = new List<AssetInfo>();
            foreach (var item in holdings)
            {
                assetInfos.Add(new AssetInfo() { Name = item.Asset.Name, Price = item.Asset.CurrentMarketPrice });
            }

            return assetInfos;
        }

        private decimal GetTotalInvestedAmoutAsync(IEnumerable<AssetTransaction> assetTransactions)
        {
            decimal totalInvestedAmount = 0;
            foreach (var transaction in assetTransactions)
            {
                if (transaction.Type == TransactionType.Buy)
                {
                    totalInvestedAmount = totalInvestedAmount + transaction.PriceAtTransaction * transaction.Quantity;
                }
                if (transaction.Type == TransactionType.Sell)
                {
                    totalInvestedAmount = totalInvestedAmount - transaction.PriceAtTransaction * transaction.Quantity;
                }
            }

            return totalInvestedAmount;
        }

        private static decimal GetCurrentTotalValue(IEnumerable<Holding> holdings)
        {
            decimal totalValue = 0;

            foreach (var item in holdings)
            {
                totalValue = totalValue + (decimal)(item?.Asset?.CurrentMarketPrice * item?.Quantity);
            }

            return totalValue;
        }
    }
}
