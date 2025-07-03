using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Assets.Queries
{
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Result<Asset>>
    {
        private readonly IAssetRepository _assetRepository;

        public GetAssetByIdQueryHandler(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task<Result<Asset>> Handle(
            GetAssetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var asset = await _assetRepository.GetAsync(request.Id);

            return Result.Success(asset);
        }
    }
}
