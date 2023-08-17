using Microsoft.EntityFrameworkCore;
using SingnalRWebApi.Domain.Repositories.Abstract;
using SingnalRWebApi.Models;



namespace SingnalRWebApi.Domain.Repositories.EntityFramework
{
    public class EFCityModelRepository : ICityModelRepository
    {
        private readonly MyDbContext _context;
        public EFCityModelRepository(MyDbContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<City>> GetListCityAsync()
        {
            return await _context.CityEntity.ToListAsync();
        }
        public async Task<City> CreateCityAsync(City city)
        {
            var currEntity = await _context.CityEntity.AddAsync(city);
            await _context.SaveChangesAsync();
            return currEntity.Entity;
        }
        public async Task<City> GetCityAsync(Guid id)
        {
            return await _context.CityEntity.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task UpdateCityAsync(City city)
        {
            _context.CityEntity.Update(city);
            await _context.SaveChangesAsync();
        }
        public async Task DeteleCityAsync(Guid id)
        {
            var client = new City() { id = id };
            _context.CityEntity.Attach(client);
            _context.CityEntity.Remove(client);
            await _context.SaveChangesAsync();
        }

    }
}
