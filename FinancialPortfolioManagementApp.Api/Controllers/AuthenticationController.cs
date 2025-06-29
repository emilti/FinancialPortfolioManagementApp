using FinancialPortfolioManagementApp.Application.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly ISender _mediator;
        //private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator) : base(mediator)
        {
            _mediator = mediator;
            //_mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            //  var command = _mapper.Map<RegisterCommand>(request);

            RegisterCommand command = new RegisterCommand(request.Email, request.Password);
            var authResult = await _mediator.Send(command);
            //var response = ApiResponse.FromResult(authResult);

            //if (response.Errors.Any())
            //{
            //    return BadRequest(String.Join(',', response.Errors));
            //}

            return Ok();// Ok(response.Message);
        }
    }
}
