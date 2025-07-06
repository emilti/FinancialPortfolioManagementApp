using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Requests;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset;
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
        private readonly IMapper _mapper;

        public AssetsController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{assetId:guid}")]
        public async Task<IActionResult> GetAssetById(Guid assetId)
        {
            var query = new GetAssetByIdQuery(assetId);
            var result = await _mediator.Send(query);

            var response = ApiResponse<Asset>.FromResult(result);

            if (response.Errors.Any())
            {
                return NotFound(new { Errors = response.Errors });
            }

            var apiResponse = Mapper.Map<ApiResponse<AssetResponse>>(response);

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest request)
        {
            var command = new CreateAssetCommand(request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var response = ApiResponse<Asset>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            var apiResponse = _mapper.Map<ApiResponse<AssetResponse>>(response);

            return CreatedAtAction(
                nameof(CreateAsset), 
                new { id = apiResponse.Data?.Id },
                response);
        }

        [HttpPut("{assetId:guid}")]
        public async Task<IActionResult> UpdateAsset(Guid assetId, [FromBody] UpdateAssetRequest request)
        {
            var command = new UpdateAssetCommand(assetId, request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var response = ApiResponse<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }

        [HttpDelete("{assetId:guid}")]
        public async Task<IActionResult> DeleteAsset(Guid assetId)
        {
            var command = new DeleteAssetCommand(assetId);
            var result = await _mediator.Send(command);

            var response = ApiResponse<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }

        [HttpPost("{assetId:guid}/buy")]
        public async Task<IActionResult> BuyAsset(Guid assetId, [FromBody] BuyAssetRequest request)
        {
            var command = new BuyAssetCommand(assetId, request.Quantity);
            var result = await _mediator.Send(command);

            var response = ApiResponse<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }

        [HttpPost("{assetId:guid}/sell")]
        public async Task<IActionResult> SellAsset(Guid assetId, [FromBody] SellAssetRequest request)
        {
            var command = new SellAssetCommand(assetId, request.Quantity);
            var result = await _mediator.Send(command);

            var response = ApiResponse<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }
    }
}
