using Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services.TariffCalculators;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Test.TariffCalculators;
public class BasicElectricityTariffCalculatorTest
{
    [Theory]
    [InlineData(3500, 5, 22, 830)]
    [InlineData(4500, 5, 22, 1050)]
    [InlineData(0, 5, 22, 60)]
    public void Calculate_InCaseOfSamples_CostEqualsExpectation(decimal consumption, decimal baseCost, decimal additionalKwhCost, decimal expectedCost)
    {
        var service = new BasicElectricityTariffCalculator();
        var config = new TariffConfigDto(TariffType.BasicElectricityTariff, "test", baseCost, additionalKwhCost, 0);

        var result = service.Calculate(consumption, config);

        Assert.Equal(expectedCost, result);
    }


    [Fact]
    public void Calculate_ConsumptionIsNegative_ThrowsException()
    {
        var service = new BasicElectricityTariffCalculator();
        var config = new TariffConfigDto(TariffType.BasicElectricityTariff, "test", 5, 22, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(-1, config));
    }

    [Fact]
    public void Calculate_ConfigTypeIsNotBasicTariff_ThrowsException()
    {
        var service = new BasicElectricityTariffCalculator();
        var config = new TariffConfigDto(TariffType.PackagedTariff, "test", 5, 22, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(10, config));
    }
}
