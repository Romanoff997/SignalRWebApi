using Microsoft.EntityFrameworkCore;
using SingnalRWebApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SingnalRWebApi.Domain.Repositories
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SingnalRWebApiDB");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<City> CityEntity { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
