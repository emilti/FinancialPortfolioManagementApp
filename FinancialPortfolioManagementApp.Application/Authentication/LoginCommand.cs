﻿using FinancialPortfolioManagementApp.Application.Shared.Common;
using MediatR;

namespace FinancialPortfolioManagementApp.Application.Authentication
{
    public record LoginCommand(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;
}
