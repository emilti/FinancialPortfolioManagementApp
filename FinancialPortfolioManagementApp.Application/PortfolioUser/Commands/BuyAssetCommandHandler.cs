using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.PortfolioUser.Commands
{
    public class BuyAsseyCommandHandler : IRequestHandler<BuyAssetCommand, Result<bool>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IAssetTransactionRepository _assetTransactionRepository;
        private readonly IAssetRepository _assetRepository;
        public BuyAsseyCommandHandler(IHoldingRepository holdingRepository, IAssetTransactionRepository assetTransactionRepository, IAssetRepository assetRepository)
        {
            _holdingRepository = holdingRepository;
            _assetTransactionRepository = assetTransactionRepository;
            _assetRepository = assetRepository;
        }

        public async Task<Result<bool>> Handle(
            BuyAssetCommand request,
            CancellationToken cancellationToken)
        {
            var holding = _holdingRepository.Get(request.UserId, request.AssetId);

            if (holding == null)
            {
                holding = new Holding()
                {
                    AssetId = request.AssetId,
                    UserId = request.UserId,
                    Quantity = request.Quantity

                };
                _holdingRepository.Add(holding);
            }
            else if (holding != null)
            {
                holding.Quantity = holding.Quantity + request.Quantity;
            }

            var asset = _assetRepository.Get(request.AssetId);

            AssetTransaction assetTransaction = new AssetTransaction()
            {
                AssetId = request.AssetId,
                UserId = request.UserId,
                PriceAtTransaction = asset.CurrentMarketPrice,
                Quantity = request.Quantity,
                Type = Domain.Enums.TransactionType.Buy,
            };

            _assetTransactionRepository.Add(assetTransaction);

            await _assetTransactionRepository.SaveAsync();

            return Result.Success(true);
        }
    }
}
