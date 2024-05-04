using MultiContactManagement.Domain.EntitiesExternal;

namespace MultiContactManagement.Application.InterfacesExternal
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllAsync();
    }
}
