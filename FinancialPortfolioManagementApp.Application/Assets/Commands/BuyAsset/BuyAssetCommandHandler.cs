using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset
{
    public class BuyAsseyCommandHandler : IRequestHandler<BuyAssetCommand, Result<bool>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IAssetTransactionRepository _assetTransactionRepository;
        private readonly IAssetRepository _assetRepository;
        public BuyAsseyCommandHandler(
            IHoldingRepository holdingRepository,
            IAssetTransactionRepository assetTransactionRepository, 
            IAssetRepository assetRepository)
        {
            _holdingRepository = holdingRepository;
            _assetTransactionRepository = assetTransactionRepository;
            _assetRepository = assetRepository;
        }

        public async Task<Result<bool>> Handle(
            BuyAssetCommand request,
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

            await using var transaction = await _holdingRepository.BeginTransactionAsync();

            try
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
                else
                {
                    holding.Quantity += request.Quantity;
                    _holdingRepository.Update(holding);
                }

                var asset = _assetRepository.Get(request.AssetId);
                if (asset == null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<bool>($"Asset {request.AssetId} not found");
                }

                var assetTransaction = new AssetTransaction()
                {
                    AssetId = request.AssetId,
                    UserId = request.UserId,
                    PriceAtTransaction = asset.CurrentMarketPrice,
                    Quantity = request.Quantity,
                    Type = Domain.Enums.TransactionType.Buy,
                };

                _assetTransactionRepository.Add(assetTransaction);
                await _holdingRepository.SaveAsync();

                await transaction.CommitAsync(cancellationToken);

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return Result.Failure<bool>($"Failed to process purchase: {ex.Message}");
            }
        }
    }
}
