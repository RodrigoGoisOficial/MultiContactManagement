using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.InterfacesExternal;
using MultiContactManagement.Domain.EntitiesExternal;

namespace MultiContactManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
        {
            var countries = await _countryService.GetAllAsync();

            return Ok(countries);
        }
    }
}
