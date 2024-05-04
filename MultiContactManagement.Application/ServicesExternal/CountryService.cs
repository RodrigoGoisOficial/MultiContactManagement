using MultiContactManagement.Application.InterfacesExternal;
using MultiContactManagement.Domain.EntitiesExternal;
using MultiContactManagement.Domain.InterfacesExternal;

namespace MultiContactManagement.Application.ServicesExternal
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAllAsync();

            return countries;
        }
    }
}
