using AutoMapper;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Api.Configurations
{
    public class AssetMappingProfile : Profile
    {
        public AssetMappingProfile()
        {
            CreateMap<Asset, AssetResponse>();
        }
    }
}
