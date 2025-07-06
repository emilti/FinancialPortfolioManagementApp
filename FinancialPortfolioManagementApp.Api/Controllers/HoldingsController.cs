using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Holdings.Query;
using FinancialPortfolioManagementApp.Application.Holdings.Query.GetHoldingsSummaryByUserId;
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
        public async Task<IActionResult> GetHoldingsSummaryAsync()
        {
            var query = new GetHoldingsSummaryQuery();
            var result = await _mediator.Send(query);
            
            if (result == null || result.Data == null)
            {
               return NotFound(new { Errors = result.Errors });
            }

            if (result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }

            var apiResult = _mapper.Map<HoldingsSummaryResponse>(result.Data);
            var apiResponse = ApiResponse<HoldingsSummaryResponse>.FromResult(apiResult);

            return Ok(apiResponse);
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetPortfolioMetricsAsync()
        {
            var query = new GetPortfolioMetricsQuery();
            var result = await _mediator.Send(query);
          
            if (result.Errors.Any())
            {
                return BadRequest(new { Errors = result.Errors });
            }

            var apiResult = _mapper.Map<PortfolioMetricsResponse>(result.Data);
            var response = ApiResponse<PortfolioMetricsResponse>.FromResult(apiResult);

            return Ok(response.Data);
        }
    }
}
