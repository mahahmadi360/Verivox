using Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Test.TariffCalculators;
public class PackagedTariffCalculatorTest
{
    [Theory]
    [InlineData(3500, 4000, 800, 30, 800)]
    [InlineData(4500, 4000, 800, 30, 950)]
    [InlineData(0, 4000, 800, 30, 800)]
    public void Calculate_InCaseOfSamples_CostEqualsExpectation(decimal consumption, decimal includeKwh, decimal baseCost, decimal additionalKwhCost, decimal expectedCost)
    {
        var service = new PackagedTariffCalculator();
        var config = new TariffConfigDto(TariffType.PackagedTariff, "test", baseCost, additionalKwhCost, includeKwh);

        var result = service.Calculate(consumption, config);

        Assert.Equal(expectedCost, result);
    }


    [Fact]
    public void Calculate_ConsumptionIsNegative_ThrowsException()
    {
        var service = new PackagedTariffCalculator();
        var config = new TariffConfigDto(TariffType.PackagedTariff, "test", 5, 22, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(-1, config));
    }

    [Fact]
    public void Calculate_ConfigTypeIsNotPackagedTariff_ThrowsException()
    {
        var service = new PackagedTariffCalculator();
        var config = new TariffConfigDto(TariffType.BasicElectricityTariff, "test", 5, 22, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(10, config));
    }
}
