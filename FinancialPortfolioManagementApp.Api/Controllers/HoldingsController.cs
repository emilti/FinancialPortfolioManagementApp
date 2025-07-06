using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Assets.Queries;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId.Models;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetPortfolioMetricsByUserId.Models;
using FinancialPortfolioManagementApp.Application.PortfolioUser.Query;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [Route("api/holdings")]
    [Authorize]
    public class HoldingsController : CustomControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public HoldingsController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHoldingsSummaryByUserIdAsync()
        {
            var query = new GetHoldingsSummaryByUserIdQuery();
            var result = await _mediator.Send(query);

            var response = ApiResponse<HoldingsSummary>.FromResult(result);

            if (response.Errors.Any())
            {
                return NotFound(new { Errors = response.Errors });
            }

            //var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            return Ok(response);
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetPortfolioMetricsByUserIdAsync([FromQuery] Guid userId)
        {
            var query = new GetPortfolioMetricsByUserIdQuery(userId);
            var result = await _mediator.Send(query);

            var response = ApiResponse<PortfolioMetrics>.FromResult(result);

            if (response.Errors.Any())
            {
                return NotFound(new { Errors = response.Errors });
            }

            //var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            return Ok(response.Data);
        }
    }
}
