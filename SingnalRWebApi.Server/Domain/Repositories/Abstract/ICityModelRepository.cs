using SignalRWebApi.Server.Models;

namespace SignalRWebApi.Domain.Repositories.Abstract
{
    public interface ICityModelRepository
    {
        public  Task<IEnumerable<CityEntity>> GetListCityAsync();
        public  Task<CityEntity> CreateCityAsync(CityEntity client);
        public Task<CityEntity> GetCityAsync(Guid id);
        public  Task UpdateCityAsync(CityEntity client);
        public  Task DeteleCityAsync(Guid id);

    }
}
