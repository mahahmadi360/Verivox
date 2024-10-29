using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;

public interface ITariffCalculator
{
    TariffType TariffType { get; }
    decimal Calculate(decimal consumption, TariffConfigDto config);
}
