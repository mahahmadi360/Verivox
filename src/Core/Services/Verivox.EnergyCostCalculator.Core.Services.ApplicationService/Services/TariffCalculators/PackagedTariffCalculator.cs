using Verivox.EnergyCostCalculator.Core.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
internal class PackagedTariffCalculator : ITariffCalculator
{
    public TariffType TariffType => TariffType.PackagedTariff;

    public decimal Calculate(decimal consumption, TariffConfigDto config)
    {
        if (consumption < 0)
            throw new ArgumentOutOfRangeException("Consumption can not be a negative number");

        if (config.Type != TariffType.PackagedTariff)
            throw new ArgumentOutOfRangeException($"Tariff config is not {nameof(TariffType.BasicElectricityTariff)}");

        var baseCosts = config.BaseCost;

        if (consumption <= config.IncludedKwh)
            return baseCosts;

        var additionConsumption = consumption - config.IncludedKwh;
        var additionalCostsPerEuro = config.AdditionalKwhCost / 100;

        var additionalCosts = additionConsumption * additionalCostsPerEuro;

        return additionalCosts + baseCosts;
    }
}
