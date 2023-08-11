using SingnalRWebApi.Domain.Repositories.Abstract;

namespace SingnalRWebApi.Domain.Repositories
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
