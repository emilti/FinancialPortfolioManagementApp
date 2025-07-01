using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Requests;
using FinancialPortfolioManagementApp.Application.Assets.Commands.CreateAsset;
using FinancialPortfolioManagementApp.Application.Assets.Commands.DeleteAsset;
using FinancialPortfolioManagementApp.Application.Assets.Commands.UpdateAsset;
using FinancialPortfolioManagementApp.Application.Assets.Queries;
using FinancialPortfolioManagementApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [Route("api/assets")]
    [Authorize]
    public class AssetsController : CustomControllerBase
    {
        private readonly ISender _mediator;

        public AssetsController(ISender mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]        
        public async Task<IActionResult> GetAssetById(Guid id)
        {
            var query = new GetAssetByIdQuery(id);
            var result = await _mediator.Send(query);

            var apiResponse = ApiResponse<Asset>.FromResult(result);

            if (apiResponse.Errors.Any())
            {
                return NotFound(string.Join(',', apiResponse.Errors));
            }

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest request)
        {
            var command = new CreateAssetCommand(request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var apiResponse = ApiResponse<Asset>.FromResult(result);

            if (apiResponse.Errors.Any())
            {
                return BadRequest(string.Join(',', apiResponse.Errors));
            }

            return Ok(apiResponse);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] UpdateAssetRequest request)
        {
            var command = new UpdateAssetCommand(id, request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var apiResponse = ApiResponse<bool>.FromResult(result);

            if (apiResponse.Errors.Any())
            {
                return BadRequest(string.Join(',', apiResponse.Errors));
            }

            return Ok(apiResponse);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var command = new DeleteAssetCommand(id);
            var result = await _mediator.Send(command);

            var apiResponse = ApiResponse<bool>.FromResult(result);

            if (apiResponse.Errors.Any())
            {
                return BadRequest(string.Join(',', apiResponse.Errors));
            }

            return NoContent();
        }
    }
}
