using Microsoft.EntityFrameworkCore;
using SignalRWebApi.Domain.Repositories.Abstract;
using SignalRWebApi.Server.Models;

namespace SignalRWebApi.Domain.Repositories.EntityFramework
{
    public class EFCityModelRepository : ICityModelRepository
    {
        private readonly MyDbContext _context;
        public EFCityModelRepository(MyDbContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<CityEntity>> GetListCityAsync()
        {
            return await _context.CityEntity.ToListAsync();
        }
        public async Task<CityEntity> CreateCityAsync(CityEntity city)
        {
            var currEntity = await _context.CityEntity.AddAsync(city);
            await _context.SaveChangesAsync();
            return currEntity.Entity;
        }
        public async Task<CityEntity> GetCityAsync(Guid id)
        {
            return await _context.CityEntity.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task UpdateCityAsync(CityEntity city)
        {
            _context.CityEntity.Update(city);
            await _context.SaveChangesAsync();
        }
        public async Task DeteleCityAsync(Guid id)
        {
            var client = new CityEntity() { id = id };
            _context.CityEntity.Attach(client);
            _context.CityEntity.Remove(client);
            await _context.SaveChangesAsync();
        }

    }
}
