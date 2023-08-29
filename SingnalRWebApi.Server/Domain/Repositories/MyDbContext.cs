using Microsoft.EntityFrameworkCore;
using SignalRWebApi.Server.Models;

namespace SignalRWebApi.Domain.Repositories
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SignalRWebApiDB");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<CityEntity> CityEntity { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
