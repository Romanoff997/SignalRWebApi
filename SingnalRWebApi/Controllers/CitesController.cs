using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SingnalRWebApi.Domain.Repositories;
using SingnalRWebApi.Hubs;
using SingnalRWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace SingnalRWebApi.Controllers
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
            var result = await _dataManager.CityRepository.GetCityAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody]City city)
        {
            City newCity = await _dataManager.CityRepository.CreateCityAsync(
                new City()
                {
                    name = city.name,
                    population = city.population,
                    fondation = city.fondation
                });
            await _cityHub.CreateCity(newCity);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(City city)
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
