using FinancialPortfolioManagementApp.Application.Extensions;
using FinancialPortfolioManagementApp.Infrastructure.Extensions;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder = WebApplication.CreateBuilder(args);
//{
//    builder.Services
//        .AddApplication()
//        .AddInfrastructure(builder.Configuration);

//}
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
   
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
