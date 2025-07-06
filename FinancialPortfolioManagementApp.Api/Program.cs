using FinancialPortfolioManagementApp.Api.Configurations;
using FinancialPortfolioManagementApp.Api.Middlewares;
using FinancialPortfolioManagementApp.Application.Holdings.Configurations;
using FinancialPortfolioManagementApp.Application.Shared.Extensions;
using FinancialPortfolioManagementApp.Infrastructure.Extensions;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                "https://localhost:5063")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AllowAnonymousFilter());

});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(HoldingApiMappingProfile));
builder.Services.AddAutoMapper(typeof(AssetApiMappingProfile));
builder.Services.AddAutoMapper(typeof(HoldingApplicationProfile));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FinancialPortfolioManagementAppDbContext>();
        await DatabaseInitializer.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseMiddleware<ExceptionMiddleware>();
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204; 
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
