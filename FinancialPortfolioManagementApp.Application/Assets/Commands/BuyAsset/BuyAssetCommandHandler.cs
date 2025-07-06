using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset
{
    public class BuyAsseyCommandHandler : IRequestHandler<BuyAssetCommand, Result<bool>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IAssetTransactionRepository _assetTransactionRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly ICurrentUserService _currentUserService;
        public BuyAsseyCommandHandler(
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
            BuyAssetCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return Result.Failure<bool>("No authenticated user");
            }

            Guid userId = new Guid(_currentUserService.UserId);
            if (request == null)
            {
                return Result.Failure<bool>("Request cannot be null");
            }

            if (request.Quantity <= 0)
            {
                return Result.Failure<bool>("Quantity must be positive");
            }

            var transaction = await _holdingRepository.BeginTransactionAsync();

            try
            {
                var holding = _holdingRepository.Get(userId, request.AssetId);

                if (holding == null)
                {
                    holding = new Holding()
                    {
                        AssetId = request.AssetId,
                        UserId = userId,
                        Quantity = request.Quantity
                    };
                    _holdingRepository.Add(holding);
                }
                else
                {
                    holding.Quantity += request.Quantity;
                    _holdingRepository.Update(holding);
                }

                var asset = await _assetRepository.GetAsync(request.AssetId);
                if (asset == null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<bool>($"Asset {request.AssetId} not found");
                }

                var assetTransaction = new AssetTransaction()
                {
                    AssetId = request.AssetId,
                    UserId = userId,
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
