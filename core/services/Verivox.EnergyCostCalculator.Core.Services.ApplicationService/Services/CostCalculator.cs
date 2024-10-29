using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService.Models;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services;

internal class CostCalculator : ICostCalulator
{
    private readonly ITariffProvider _tariffProvider;
    private readonly IEnumerable<ITariffCalculator> _tariffCalulators;

    public CostCalculator(ITariffProvider tariffProvider,
        IEnumerable<ITariffCalculator> tariffCalulators)
    {
        _tariffProvider = tariffProvider;
        _tariffCalulators = tariffCalulators;
    }

    public async Task<IReadOnlyCollection<ConsumptionCostCalculationResult>> Calculate(decimal consumption)
    {
        var tariffs = await _tariffProvider.GetTariffs();

        var tariffAndCalculators = GetTariffCalculators(tariffs);

        var result = CalculateCosts(consumption, tariffAndCalculators);

        return result;
    }

    private List<(TariffConfigDto tariff, ITariffCalculator calculator)> GetTariffCalculators(IReadOnlyCollection<TariffConfigDto> tariffs)
    {
        return tariffs.Select(tariff =>
        {
            var calculators = _tariffCalulators.Where(a => a.TariffType == tariff.Type);

            if (!calculators.Any())
                throw new ArgumentException($"Tarrif calculator for {tariff.Type.ToString()} not defined");

            return (tariff, calculators);
        })
            .SelectMany(tariffCalculators => tariffCalculators.calculators
                .Select(calculator => (tariffCalculators.tariff, calculator))
            ).ToList();
    }

    private IReadOnlyCollection<ConsumptionCostCalculationResult> CalculateCosts(decimal consumption, List<(TariffConfigDto tariff, ITariffCalculator calculator)> tariffAndCalculators)
    {
        return tariffAndCalculators.AsParallel()
                    .Select(tariffCalculator =>
                    {
                        var totalCost = tariffCalculator.calculator.Calculate(consumption, tariffCalculator.tariff);
                        return new ConsumptionCostCalculationResult(tariffCalculator.tariff.Name, totalCost);
                    })
                    .OrderBy(a => a.AnnualCost)
                    .ToArray();
    }
}
