namespace Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;
public enum TariffType
{
    BasicElectricityTariff = 1,
    PackagedTariff = 2
}

public record TariffDto(TariffType Type,
        string Name,
        decimal BaseCost,
        decimal AdditionalKwhCost,
        decimal? IncludedKwh)
{ }
