using Verivox.EnergyCostCalculator.Infrastructure.ExternalServices;
using Verivox.EnergyCostCalculator.Core.Services.ApplicationService;
using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExternalServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("api/getCosts/{usage:decimal}", (decimal usage, ICostCalulator costCalculators) =>
{
    return costCalculators.Calculate(usage);
});

app.Run();
