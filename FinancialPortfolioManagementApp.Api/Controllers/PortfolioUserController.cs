using AutoMapper;
using FinancialPortfolioManagementApp.Api.Models.PortfolioUser.Requests;
using FinancialPortfolioManagementApp.Application.PortfolioUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    public class PortfolioUserController : CustomControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PortfolioUserController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> BuyAsset([FromBody] BuyAssetRequest request)
        {
            var command = new BuyAssetCommand(request.UserId, request.AsetId, request.Quantity);
            var result = await _mediator.Send(command);
            return null;
            //var response = Response<Asset>.FromResult(result);

            //if (response.Errors.Any())
            //{
            //    return BadRequest(string.Join(',', response.Errors));
            //}

            //var apiResponse = Mapper.Map<Response<AssetResponse>>(response);

            //return CreatedAtAction(
            //    nameof(GetAssetById),
            //    new { id = apiResponse.Data?.Id },
            //    response);
        }
    }
}
