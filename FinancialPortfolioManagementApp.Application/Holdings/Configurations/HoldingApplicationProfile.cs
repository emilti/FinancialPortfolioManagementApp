using AutoMapper;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;
using FinancialPortfolioManagementApp.Domain.Entities;

namespace FinancialPortfolioManagementApp.Application.Holdings.Configurations
{
    public class HoldingApplicationProfile : Profile
    {
        public HoldingApplicationProfile()
        {
            CreateMap<Asset, AssetDto>();
            CreateMap<Holding, HoldingDto>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty));
        }
    }
}