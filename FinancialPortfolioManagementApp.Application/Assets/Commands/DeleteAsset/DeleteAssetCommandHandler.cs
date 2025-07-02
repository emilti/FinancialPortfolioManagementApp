using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, Result<bool>>
    {
        private readonly IAssetRepository _assetRepository;

        public DeleteAssetCommandHandler(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task<Result<bool>> Handle(
            DeleteAssetCommand request,
            CancellationToken cancellationToken)
        {
            Asset asset = _assetRepository.Get(request.id);
            _assetRepository.Delete(asset);
            await _assetRepository.SaveAsync();

            return Result.Success(true);
        }
    }
}
