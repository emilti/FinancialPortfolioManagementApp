using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Api.Models.Assets.Requests;
using FinancialPortfolioManagementApp.Api.Models.Assets.Response;
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAssetById(Guid id)
        {
            var query = new GetAssetByIdQuery(id);
            var result = await _mediator.Send(query);

            var response = Response<Asset>.FromResult(result);

            if (response.Errors.Any())
            {
                return NotFound(new { Errors = response.Errors });
            }

            var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest request)
        {
            var command = new CreateAssetCommand(request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var response = Response<Asset>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            return CreatedAtAction(
                nameof(GetAssetById), 
                new { id = apiResponse.Data?.Id },
                response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] UpdateAssetRequest request)
        {
            var command = new UpdateAssetCommand(id, request.Name, request.CurrentMarketPrice);
            var result = await _mediator.Send(command);

            var response = Response<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var command = new DeleteAssetCommand(id);
            var result = await _mediator.Send(command);

            var response = Response<bool>.FromResult(result);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(',', response.Errors));
            }

            return NoContent();
        }
    }
}
