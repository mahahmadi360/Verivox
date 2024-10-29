using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Contracts.ApplicationService;

public interface ITariffCalculator
{
    TariffType TariffType { get; }
    decimal Calculate(decimal consumption, TariffConfigDto config);
}
