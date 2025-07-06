using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;

namespace FinancialPortfolioManagementApp.Api.Configurations
{
    public class HoldingApiMappingProfile : Profile
    {
        public HoldingApiMappingProfile()
        {
            CreateMap<HoldingDto, HoldingResponse>();
            CreateMap<HoldingsSummary, HoldingsSummaryResponse>();

            CreateMap<AssetInfo, AssetInfoResponse>();
            CreateMap<PortfolioMetrics, PortfolioMetricsResponse>();
            CreateMap<ApiResponse<PortfolioMetrics>, ApiResponse<PortfolioMetricsResponse>>()
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
              .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors))
              .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success));

            CreateMap<ApiResponse<HoldingsSummary>, ApiResponse<HoldingsSummaryResponse>>()
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
              .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors))
              .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success));
        }
    }
}
