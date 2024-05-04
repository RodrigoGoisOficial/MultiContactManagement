using MultiContactManagement.Domain.EntitiesExternal;
using MultiContactManagement.Domain.InterfacesExternal;
using Newtonsoft.Json;

namespace MultiContactManagement.Infra.RepositoriesExternal
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string urlApi = "https://restcountries.com/v3.1/all";
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Country>> GetAllAsync()
        {
            var response = await _httpClient.GetStringAsync(urlApi);
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(response);

            foreach (var country in countries)
            {
                var root = country.Idd.Root ?? string.Empty;
                var suffix = country.Idd.Suffixes != null && country.Idd.Suffixes.Count > 0 ? country.Idd.Suffixes[0] : string.Empty;
                country.CountryCode = root + suffix;
            }

            return countries;
        }

    }
}
