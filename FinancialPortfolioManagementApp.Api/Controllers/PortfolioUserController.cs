using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Assets.Queries;
using FinancialPortfolioManagementApp.Application.PortfolioUser.Query;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [Route("api/portfolio-users")]
    [Authorize]
    public class PortfolioUserController : CustomControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PortfolioUserController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetAssetById(Guid userId)
        {
            var query = new GetHoldingsSummaryByUserIdQuery(userId);
            var result = await _mediator.Send(query);

            var response = Response<PortfolioSummary>.FromResult(result);

            if (response.Errors.Any())
            {
                return NotFound(new { Errors = response.Errors });
            }

            //var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            return Ok(response);
        }
    }
}
