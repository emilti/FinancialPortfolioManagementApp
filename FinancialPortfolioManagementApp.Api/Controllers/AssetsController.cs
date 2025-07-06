using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Requests;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
using FinancialPortfolioManagementApp.Application.Assets.Commands.BuyAsset;
using FinancialPortfolioManagementApp.Application.Assets.Commands.CreateAsset;
using FinancialPortfolioManagementApp.Application.Assets.Commands.DeleteAsset;
using FinancialPortfolioManagementApp.Application.Assets.Commands.UpdateAsset;
using FinancialPortfolioManagementApp.Application.Assets.Queries;
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
        public async Task<IActionResult> GetAssetByIdAsync(Guid assetId)
        {
            if (assetId == Guid.Empty)
            {
                return BadRequest("Invalid asset ID");
            }

            var query = new GetAssetByIdQuery(assetId);
            var result = await _mediator.Send(query);

            if (result == null || result.Data == null)
            {
                return NotFound(new { Errors = result.Errors });
            }

            if (result.Errors.Any())
            {
                return BadRequest(new { Errors = result.Errors });
            }

            var apiResult = _mapper.Map<AssetResponse>(result.Data);
            var apiResponse = ApiResponse<AssetResponse>.FromResult(apiResult);

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssetAsync([FromBody] CreateAssetRequest request)
        {
            if (request == null || String.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid request");
            }

            var command = new CreateAssetCommand(request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            if (result == null || result.Data == null)
            {
                return NotFound();
            }

            if (result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }

            var apiResult = _mapper.Map<AssetResponse>(result.Data);
            var apiResponse = ApiResponse<AssetResponse>.FromResult(apiResult);

            return Ok(apiResponse);
        }

        [HttpPut("{assetId:guid}")]
        public async Task<IActionResult> UpdateAssetAsync(Guid assetId, [FromBody] UpdateAssetRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid request");
            }

            var command = new UpdateAssetCommand(assetId, request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }

            if (result == null || result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }

            var apiResponse = ApiResponse<bool>.FromResult(result);

            return NoContent();
        }

        [HttpDelete("{assetId:guid}")]
        public async Task<IActionResult> DeleteAssetAsync(Guid assetId)
        {
            var command = new DeleteAssetCommand(assetId);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            if (result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }

            if (result.Data == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{assetId:guid}/buy")]
        public async Task<IActionResult> BuyAssetAsync(Guid assetId, [FromBody] BuyAssetRequest request)
        {
            if (request.Quantity <= 0)
            {
                return BadRequest("Invalid request");
            }

            var command = new BuyAssetCommand(assetId, request.Quantity);
            var result = await _mediator.Send(command);

            if (result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }

            if (result.Data == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{assetId:guid}/sell")]
        public async Task<IActionResult> SellAssetAsync(Guid assetId, [FromBody] SellAssetRequest request)
        {
            if (request.Quantity <= 0)
            {
                return BadRequest("Invalid request");
            }

            var command = new SellAssetCommand(assetId, request.Quantity);
            var result = await _mediator.Send(command);

            if (result.Errors.Any())
            {
                return BadRequest(string.Join(',', result.Errors));
            }


            if (result.Data == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
