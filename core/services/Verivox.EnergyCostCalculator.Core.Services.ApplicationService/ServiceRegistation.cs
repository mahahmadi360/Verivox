using Microsoft.Extensions.DependencyInjection;
using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services;
using Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService;
public static class ServiceRegistation
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        // define tariff calculators
        serviceCollection.AddScoped<ITariffCalculator, BasicElectricityTariffCalculator>();
        serviceCollection.AddScoped<ITariffCalculator, PackagedTariffCalculator>();

        serviceCollection.AddScoped<ICostCalulator, CostCalculator>();
    }
}
