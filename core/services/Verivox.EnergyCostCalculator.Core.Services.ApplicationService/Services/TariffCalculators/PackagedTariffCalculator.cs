using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
internal class PackagedTariffCalculator : ITariffCalculator
{
    public TariffType TariffType => TariffType.PackagedTariff;

    public decimal Calculate(decimal consumption, TariffConfigDto config)
    {
        var baseCosts = config.BaseCost;

        if (consumption <= config.IncludedKwh)
            return baseCosts;

        var additionConsumption = consumption - config.IncludedKwh;
        var additionalCostsPerEuro = config.AdditionalKwhCost / 100;

        var additionalCosts = additionConsumption * additionalCostsPerEuro;

        return additionalCosts + baseCosts;
    }
}
