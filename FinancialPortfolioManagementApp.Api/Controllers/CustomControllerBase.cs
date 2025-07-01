using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public abstract class CustomControllerBase(ISender mediator, IMapper mapper) : ControllerBase
    {
        protected ISender Mediator { get; } = mediator;

        protected IMapper Mapper { get; } = mapper;
    }
}
