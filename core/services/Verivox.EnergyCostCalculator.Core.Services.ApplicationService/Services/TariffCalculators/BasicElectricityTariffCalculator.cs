using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
internal class BasicElectricityTariffCalculator : ITariffCalculator
{
    public TariffType TariffType => TariffType.BasicElectricityTariff;

    public decimal Calculate(decimal consumption, TariffConfigDto config)
    {
        var baseCosts = config.BaseCost * 12;
        var additionalCostsPerEuro = config.AdditionalKwhCost / 100;

        var consumptionCosts = additionalCostsPerEuro * consumption ;

        return baseCosts + consumptionCosts;
    }
}
