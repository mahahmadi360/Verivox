using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService.Models;

namespace Verivox.EnergyCostCalculator.Core.Contracts.ApplicationService;
public interface ICostCalulator
{
    Task<IReadOnlyCollection<ConsumptionCostCalculationResult>> Calculate(decimal consumption);
}
