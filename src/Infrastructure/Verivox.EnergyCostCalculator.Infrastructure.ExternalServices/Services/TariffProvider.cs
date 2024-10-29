using Verivox.EnergyCostCalculator.Infrastructure.Contracts;
using Verivox.EnergyCostCalculator.Infrastructure.Contracts.Models;

namespace Verivox.EnergyCostCalculator.Infrastructure.ExternalServices.Services;
internal class TariffProvider : ITariffProvider
{
    public Task<IReadOnlyCollection<TariffConfigDto>> GetTariffs()
    {
        //todo get data from external service
        IReadOnlyCollection<TariffConfigDto> tariffs = new TariffConfigDto[]
        {
            new TariffConfigDto(TariffType.BasicElectricityTariff,"Product A",5,22,0),
            new TariffConfigDto(TariffType.PackagedTariff,"Product B",800, 30, 4000)
        }.AsReadOnly();

        return Task.FromResult(tariffs);
    }
}
