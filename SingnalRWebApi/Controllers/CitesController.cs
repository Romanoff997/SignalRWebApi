using Microsoft.AspNetCore.Mvc;
using SignalRWebApi.Domain.Repositories;
using SignalRWebApi.Hubs;
using SignalRWebApi.Server.Models;
using SignalRWebApi.Shared.Models;

namespace SignalRWebApi.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataManager _dataManager;
        private readonly CityNotificationHub _cityHub;
        public CityController(DataManager dataManager, CityNotificationHub hubContext)
        {
            _dataManager = dataManager;
            _cityHub = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetListCity()
        {
            var result = await _dataManager.CityRepository.GetListCityAsync();
            if(result.Count()==0)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(Guid id)
        {
            try 
            { 
            var result = await _dataManager.CityRepository.GetCityAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityEntity city)
        {
            CityEntity newCity = await _dataManager.CityRepository.CreateCityAsync(
                new ()
                {
                    name = city.name,
                    population = city.population,
                    fondation = city.fondation
                });
            await _cityHub.CreateCity(newCity);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(CityEntity city)
        {
            await _dataManager.CityRepository.UpdateCityAsync(city);
            await _cityHub.UpdateCity(city);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _dataManager.CityRepository.DeteleCityAsync(id);
            await _cityHub.DeleteCity(id);
            return Ok();
        }
    }
}
