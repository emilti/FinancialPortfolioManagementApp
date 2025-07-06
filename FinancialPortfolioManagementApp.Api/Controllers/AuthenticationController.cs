using AutoMapper;
using FinancialPortfolioManagementApp.Api.Common;
using FinancialPortfolioManagementApp.Application.Authentication;
using FinancialPortfolioManagementApp.Application.Authentication.Commands.LoginCommand;
using FinancialPortfolioManagementApp.Application.Authentication.Commands.RegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagementApp.Api.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    [EnableCors("AllowFrontend")]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            RegisterCommand command = new RegisterCommand(request.Email, request.Password);
            var result = await _mediator.Send(command);
            
            if (result.Errors.Any())
            {
                return BadRequest(String.Join(',', result.Errors));
            }

            var response = ApiResponse<AuthenticationResult>.FromResult(result);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            LoginCommand command = new LoginCommand(request.Email, request.Password);
            var result = await _mediator.Send(command);

            if (result.Errors.Any())
            {
                return BadRequest(String.Join(',', result.Errors));
            }

            var response = ApiResponse<AuthenticationResult>.FromResult(result);

            return Ok(response);
        }
    }
}
