using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Infrastructure.Contracts;

public interface ITariffProvider
{
    Task<IReadOnlyCollection<TariffDto>> GetTariffs();
}
