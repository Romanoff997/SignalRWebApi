using SignalRWebApi.Domain.Repositories.Abstract;

namespace SignalRWebApi.Domain.Repositories
{
    public class DataManager
    {
        public ICityModelRepository CityRepository { get; set; }

        public DataManager(ICityModelRepository cityRepository)
        {
            CityRepository = cityRepository;
        }
    }
}
