using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public abstract class CustomControllerBase(ISender mediator) : ControllerBase
    {
        protected ISender Mediator { get; } = mediator;
    }
}
