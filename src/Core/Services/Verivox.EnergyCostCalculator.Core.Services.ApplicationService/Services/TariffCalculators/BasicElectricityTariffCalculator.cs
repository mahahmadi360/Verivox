using Verivox.EnergyCostCalculator.Core.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
internal class BasicElectricityTariffCalculator : ITariffCalculator
{
    public TariffType TariffType => TariffType.BasicElectricityTariff;

    public decimal Calculate(decimal consumption, TariffConfigDto config)
    {
        if (consumption < 0)
            throw new ArgumentOutOfRangeException("Consumption can not be a negative number");

        if (config.Type != TariffType.BasicElectricityTariff)
            throw new ArgumentOutOfRangeException($"Tariff config is not {nameof(TariffType.BasicElectricityTariff)}");

        var baseCosts = config.BaseCost * 12;
        var additionalCostsPerEuro = config.AdditionalKwhCost / 100;

        var consumptionCosts = additionalCostsPerEuro * consumption;

        return baseCosts + consumptionCosts;
    }
}
