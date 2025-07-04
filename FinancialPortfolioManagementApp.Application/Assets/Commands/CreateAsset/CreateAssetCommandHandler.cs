﻿using FinancialPortfolioManagementApp.Application.Shared.Common;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;
using FinancialPortfolioManagementApp.Application.Shared.Contracts;

namespace FinancialPortfolioManagementApp.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Result<Asset>>
    {
        private readonly IAssetRepository _assetRepository;

        public CreateAssetCommandHandler(IAssetRepository assetRepository)
            => _assetRepository = assetRepository;

        public async Task<Result<Asset>> Handle(
            CreateAssetCommand request,
            CancellationToken cancellationToken)
        {
            var asset = new Asset() { Name = request.Name, CurrentMarketPrice = request.CurrentMarketPrice };

            _assetRepository.Add(asset);
            await _assetRepository.SaveAsync();

            return Result.Success(asset);
        }
    }
}
