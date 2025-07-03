using FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.SellAsset
{
    public class SellAssetCommandHandler : IRequestHandler<SellAssetCommand, Result<bool>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IAssetTransactionRepository _assetTransactionRepository;
        private readonly IAssetRepository _assetRepository;
        private ICurrentUserService _currentUserService;
        public SellAssetCommandHandler(
            IHoldingRepository holdingRepository,
            IAssetTransactionRepository assetTransactionRepository,
            IAssetRepository assetRepository,
            ICurrentUserService currentUserService)
        {
            _holdingRepository = holdingRepository;
            _assetTransactionRepository = assetTransactionRepository;
            _assetRepository = assetRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(
            SellAssetCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Failure<bool>("Request cannot be null");
            }

            if (request.Quantity <= 0)
            {
                return Result.Failure<bool>("Quantity must be positive");
            }

            var transaction =  await _holdingRepository.BeginTransactionAsync();

            try
            {
                if (!_currentUserService.IsAuthenticated)
                {
                    return Result.Failure<bool>("No authenticated user");
                }

                if (new Guid(_currentUserService.UserId) != request.UserId)
                {
                    return Result.Failure<bool>("Invalid request. Performed action is not authorized.");
                }

                var holding = _holdingRepository.Get(request.UserId, request.AssetId);

                if (holding == null)
                {
                    return Result.Failure<bool>("Holding must exist in order to Sell asset from it");
                }

                if (holding.Quantity < request.Quantity)
                {
                    return Result.Failure<bool>("Invalid operation. Can not sell more quantity than actual holding quantity of this asset.");
                }

                var asset = await _assetRepository.GetAsync(request.AssetId);
                if (asset == null)
                {
                    return Result.Failure<bool>($"Asset {request.AssetId} not found");
                }

                holding.Quantity = holding.Quantity - request.Quantity;
                var assetTransaction = new AssetTransaction()
                {
                    AssetId = request.AssetId,
                    UserId = request.UserId,
                    PriceAtTransaction = asset.CurrentMarketPrice,
                    Quantity = request.Quantity,
                    Type = Domain.Enums.TransactionType.Sell,
                };
                _assetTransactionRepository.Add(assetTransaction);
               
                await _holdingRepository.SaveAsync();

                await transaction.CommitAsync(cancellationToken);

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return Result.Failure<bool>($"Failed to process sell: {ex.Message}");
            }
        }
    }
}
