using FinancialPortfolioManagementApp.Application.Assets.Queries.GetAllAssets;
using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Queries
{
    public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, Result<List<Asset>>>
    {
        private readonly IAssetRepository _assetRepository;

        public GetAllAssetsQueryHandler(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task<Result<List<Asset>>> Handle(
            GetAllAssetsQuery request,
            CancellationToken cancellationToken)
        {
            var asset = await _assetRepository.GetAllAsync();

            return Result.Success(asset);
        }
    }
}
