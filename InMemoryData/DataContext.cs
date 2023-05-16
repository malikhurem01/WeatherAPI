using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI.InMemoryData
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UsersDb");
        }

        public DbSet<User> Users { get; set; }

    }
}
