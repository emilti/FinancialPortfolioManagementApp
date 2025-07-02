using FinancialPortfolioManagementApp.Api.Configurations;
using FinancialPortfolioManagementApp.Api.Middlewares;
using FinancialPortfolioManagementApp.Application.Holdings.Configurations;
using FinancialPortfolioManagementApp.Application.Shared.Extensions;
using FinancialPortfolioManagementApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AllowAnonymousFilter());

});

builder.Services.AddAutoMapper(typeof(AssetMappingProfile));
builder.Services.AddAutoMapper(typeof(HoldingsProfile));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();


app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
