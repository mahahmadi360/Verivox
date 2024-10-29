using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService.Models;

namespace Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
public interface ICostCalulator
{
    Task<IReadOnlyCollection<ConsumptionCostCalculationResult>> Calculate(decimal consumption);
}
