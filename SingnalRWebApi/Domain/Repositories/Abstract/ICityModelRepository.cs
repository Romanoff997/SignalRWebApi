using SingnalRWebApi.Shared.Models;

namespace SingnalRWebApi.Domain.Repositories.Abstract
{
    public interface ICityModelRepository
    {
        public  Task<IEnumerable<City>> GetListCityAsync();
        public  Task<City> CreateCityAsync(City client);
        public Task<City> GetCityAsync(Guid id);
        public  Task UpdateCityAsync(City client);
        public  Task DeteleCityAsync(Guid id);

    }
}
