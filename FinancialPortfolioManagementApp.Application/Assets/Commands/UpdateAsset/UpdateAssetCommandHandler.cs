using FinancialPortfolioManagementApp.Application.Common;
using FinancialPortfolioManagementApp.Application.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, Result<bool>>
    {
        private readonly IAssetRepository _assetRepository;

        public UpdateAssetCommandHandler(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task<Result<bool>> Handle(
            UpdateAssetCommand request,
            CancellationToken cancellationToken)
        {
            var asset = new Asset() { Id = request.Id, Name = request.Name, CurrentMarketPrice = request.CurrentMarketPrice };

            _assetRepository.Update(asset);
            await _assetRepository.SaveAsync();

            return Result.Success(true);
        }
    }
}
