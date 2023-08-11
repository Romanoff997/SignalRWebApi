
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingnalRWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
