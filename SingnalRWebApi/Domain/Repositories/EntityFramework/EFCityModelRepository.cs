using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingnalRWebApi.Domain.Repositories.Abstract;
using SingnalRWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.CityEntity.Where(x => x.id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task UpdateCityAsync(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
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
