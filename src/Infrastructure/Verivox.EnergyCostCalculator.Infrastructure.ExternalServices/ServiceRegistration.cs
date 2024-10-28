using Microsoft.Extensions.DependencyInjection;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts;
using Verivox.EnergyCostCalculator.Infrastructure.ExternalServices.Services;

namespace Verivox.EnergyCostCalculator.Infrastructure.ExternalServices;

public static class ServiceRegistration
{
    public static void AddExternalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITariffProvider, TariffProvider>();
    }
}
