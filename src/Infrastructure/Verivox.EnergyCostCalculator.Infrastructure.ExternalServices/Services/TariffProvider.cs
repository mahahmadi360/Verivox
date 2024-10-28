using Verivox.EnergyCostCalculator.Infrastructure.Contracts;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Infrastructure.ExternalServices.Services;
internal class TariffProvider : ITariffProvider
{
    public Task<IReadOnlyCollection<TariffDto>> GetTariffs()
    {
        //todo get data from external service
        IReadOnlyCollection<TariffDto> tariffs = new TariffDto[]
        {
            new TariffDto(TariffType.BasicElectricityTariff,"Product A",5,22,null),
            new TariffDto(TariffType.PackagedTariff,"Product B",800, 30, 4000)
        }.AsReadOnly();

        return Task.FromResult(tariffs);
    }
}
