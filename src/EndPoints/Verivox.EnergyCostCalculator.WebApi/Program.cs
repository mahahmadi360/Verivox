using Verivox.EnergyCostCalculator.Infrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExternalServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("api/getCosts/{usage:decimal}", () =>
{
    throw new NotImplementedException();
});

app.Run();
