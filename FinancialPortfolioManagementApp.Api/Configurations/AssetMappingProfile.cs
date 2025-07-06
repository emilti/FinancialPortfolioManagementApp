using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Api.Configurations
{
    public class AssetMappingProfile : Profile
    {
        public AssetMappingProfile()
        {
            CreateMap<Asset, AssetResponse>();
            CreateMap<ApiResponse<Asset>, ApiResponse<AssetResponse>>()
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
              .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors))
              .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success));
        }
    }
}
