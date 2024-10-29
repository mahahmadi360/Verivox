using Moq;
using Verivox.EnergyCostCalculator.Coe.Contracts.ApplicationService;
using Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Services;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Core.Services.ApplicationService.Test;

public class CostCalculatorTest
{
    [Fact]
    public async Task Calculate_ValidateCallsAndResults()
    {
        var tariffProviderMock = new Mock<ITariffProvider>();
        var basicTarifConfig = GenerateTariffConfig("basicTarif", TariffType.BasicElectricityTariff);
        var packagedTarifConfig = GenerateTariffConfig("packaged Tarif", TariffType.PackagedTariff);
        tariffProviderMock.Setup(a => a.GetTariffs()).ReturnsAsync(new TariffConfigDto[]
        {
            basicTarifConfig,
            packagedTarifConfig
        });
        var basicCalculateCosts = 100;
        var basicTarifCaculatorMock = GenerateTariffCalculatorMock(basicCalculateCosts, TariffType.BasicElectricityTariff);
        var packagedCalculatedCosts = 200;
        Mock<ITariffCalculator> packagedTarifCaculatorMock = GenerateTariffCalculatorMock(packagedCalculatedCosts, TariffType.PackagedTariff);
        var service = new CostCalculator(tariffProviderMock.Object, new ITariffCalculator[]
        {
            basicTarifCaculatorMock.Object,
            packagedTarifCaculatorMock.Object
        });
        var consumtion = 0;

        var results = await service.Calculate(consumtion);

        Assert.Equal(2, results.Count);
        Assert.Contains(results, result => result.TariffName == basicTarifConfig.Name && result.AnnualCost == basicCalculateCosts);
        Assert.Contains(results, result => result.TariffName == packagedTarifConfig.Name && result.AnnualCost == packagedCalculatedCosts);
        basicTarifCaculatorMock.Verify(a => a.Calculate(consumtion, basicTarifConfig), Times.Once());
        packagedTarifCaculatorMock.Verify(a => a.Calculate(consumtion, packagedTarifConfig), Times.Once());
    }

    [Fact]
    public async Task Calculate_IfMultipleTariffConfigForSameType_CalculateAllCosts()
    {
        var tariffProviderMock = new Mock<ITariffProvider>();
        var basicTarifConfig1 = GenerateTariffConfig("basicTarif1", TariffType.BasicElectricityTariff);
        var basicTarifConfig2 = GenerateTariffConfig("basicTarif2", TariffType.BasicElectricityTariff);
        var packagedTarifConfig = GenerateTariffConfig("packaged Tarif", TariffType.PackagedTariff);
        tariffProviderMock.Setup(a => a.GetTariffs()).ReturnsAsync(new TariffConfigDto[]
        {
            basicTarifConfig1,
            basicTarifConfig2,
            packagedTarifConfig
        });
        var basicCalculateCosts1 = 100;
        var basicCalculateCosts2 = 100;
        var basicTarifCaculatorMock = GenerateTariffCalculatorMock(0, TariffType.BasicElectricityTariff);
        basicTarifCaculatorMock.Setup(a => a.Calculate(It.IsAny<decimal>(), basicTarifConfig1)).Returns(basicCalculateCosts1);
        basicTarifCaculatorMock.Setup(a => a.Calculate(It.IsAny<decimal>(), basicTarifConfig2)).Returns(basicCalculateCosts2);
        var packagedCalculatedCosts = 200;
        Mock<ITariffCalculator> packagedTarifCaculatorMock = GenerateTariffCalculatorMock(packagedCalculatedCosts, TariffType.PackagedTariff);
        var service = new CostCalculator(tariffProviderMock.Object, new ITariffCalculator[]
        {
            basicTarifCaculatorMock.Object,
            packagedTarifCaculatorMock.Object
        });
        var consumtion = 0;

        var results = await service.Calculate(consumtion);

        Assert.Contains(results, result => result.TariffName == basicTarifConfig1.Name && result.AnnualCost == basicCalculateCosts1);
        Assert.Contains(results, result => result.TariffName == basicTarifConfig2.Name && result.AnnualCost == basicCalculateCosts2);
        Assert.Contains(results, result => result.TariffName == packagedTarifConfig.Name && result.AnnualCost == packagedCalculatedCosts);
        basicTarifCaculatorMock.Verify(a => a.Calculate(consumtion, basicTarifConfig1), Times.Once());
        basicTarifCaculatorMock.Verify(a => a.Calculate(consumtion, basicTarifConfig2), Times.Once());
        packagedTarifCaculatorMock.Verify(a => a.Calculate(consumtion, packagedTarifConfig), Times.Once());
    }

    [Fact]
    public async Task Calculate_IfMultipleTariffCalculatorForSameType_CalculateAllCosts()
    {
        var tariffProviderMock = new Mock<ITariffProvider>();
        var basicTarifConfig = GenerateTariffConfig("basicTarif", TariffType.BasicElectricityTariff);
        var packagedTarifConfig = GenerateTariffConfig("packaged Tarif", TariffType.PackagedTariff);
        tariffProviderMock.Setup(a => a.GetTariffs()).ReturnsAsync(new TariffConfigDto[]
        {
            basicTarifConfig,
            packagedTarifConfig
        });
        var basicCalculateCosts1 = 100;
        var basicCalculateCosts2 = 100;
        var basicTarifCaculatorMock1 = GenerateTariffCalculatorMock(basicCalculateCosts1, TariffType.BasicElectricityTariff);
        var basicTarifCaculatorMock2 = GenerateTariffCalculatorMock(basicCalculateCosts2, TariffType.BasicElectricityTariff);
        var packagedCalculatedCosts = 200;
        Mock<ITariffCalculator> packagedTarifCaculatorMock = GenerateTariffCalculatorMock(packagedCalculatedCosts, TariffType.PackagedTariff);
        var service = new CostCalculator(tariffProviderMock.Object, new ITariffCalculator[]
        {
            basicTarifCaculatorMock1.Object,
            basicTarifCaculatorMock2.Object,
            packagedTarifCaculatorMock.Object
        });
        var consumtion = 0;

        var results = await service.Calculate(consumtion);

        Assert.Contains(results, result => result.TariffName == basicTarifConfig.Name && result.AnnualCost == basicCalculateCosts1);
        Assert.Contains(results, result => result.TariffName == basicTarifConfig.Name && result.AnnualCost == basicCalculateCosts2);
        Assert.Contains(results, result => result.TariffName == packagedTarifConfig.Name && result.AnnualCost == packagedCalculatedCosts);
        basicTarifCaculatorMock1.Verify(a => a.Calculate(consumtion, basicTarifConfig), Times.Once());
        basicTarifCaculatorMock2.Verify(a => a.Calculate(consumtion, basicTarifConfig), Times.Once());
        packagedTarifCaculatorMock.Verify(a => a.Calculate(consumtion, packagedTarifConfig), Times.Once());
    }

    [Fact]
    public void Calculate_IfTarifCalculatorNotDefined_ThrowException()
    {
        var tariffProviderMock = new Mock<ITariffProvider>();
        var basicTarifConfig = GenerateTariffConfig("basicTarif", TariffType.BasicElectricityTariff);
        var packagedTarifConfig = GenerateTariffConfig("packaged Tarif", TariffType.PackagedTariff);
        tariffProviderMock.Setup(a => a.GetTariffs()).ReturnsAsync(new TariffConfigDto[]
        {
            basicTarifConfig,
            packagedTarifConfig
        });
        var basicTarifCaculatorMock = GenerateTariffCalculatorMock(100, TariffType.BasicElectricityTariff);
        var service = new CostCalculator(tariffProviderMock.Object, new ITariffCalculator[]
        {
            basicTarifCaculatorMock.Object
        });
        var consumtion = 0;

        Assert.ThrowsAsync<ArgumentException>(() => service.Calculate(consumtion));
    }

    [Fact]
    public void Calculate_IfConsumptionIsNegative_ThrowException()
    {
        var tariffProviderMock = new Mock<ITariffProvider>();
        var basicTarifConfig = GenerateTariffConfig("basicTarif", TariffType.BasicElectricityTariff);
        var packagedTarifConfig = GenerateTariffConfig("packaged Tarif", TariffType.PackagedTariff);
        tariffProviderMock.Setup(a => a.GetTariffs()).ReturnsAsync(new TariffConfigDto[]
        {
            basicTarifConfig,
            packagedTarifConfig
        });
        var basicTarifCaculatorMock = GenerateTariffCalculatorMock(100, TariffType.BasicElectricityTariff);
        var service = new CostCalculator(tariffProviderMock.Object, new ITariffCalculator[]
        {
            basicTarifCaculatorMock.Object
        });

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.Calculate(-1));

        tariffProviderMock.Verify(a=> a.GetTariffs(),Times.Never());
        basicTarifCaculatorMock.Verify(a => a.Calculate(It.IsAny<decimal>(), It.IsAny<TariffConfigDto>()), Times.Never());
    }

    private static Mock<ITariffCalculator> GenerateTariffCalculatorMock(int packagedCalculatedCosts, TariffType tariffType)
    {
        var packagedTarifCaculatorMock = new Mock<ITariffCalculator>();
        packagedTarifCaculatorMock.SetupGet(a => a.TariffType).Returns(tariffType);
        packagedTarifCaculatorMock.Setup(a => a.Calculate(It.IsAny<decimal>(), It.IsAny<TariffConfigDto>())).Returns(packagedCalculatedCosts);
        return packagedTarifCaculatorMock;
    }

    private TariffConfigDto GenerateTariffConfig(string name, TariffType tariffType)
    {
        return new TariffConfigDto(tariffType, name, 0, 0, 0);
    }
}