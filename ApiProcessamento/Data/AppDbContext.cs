using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiProcessamento.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensorData> SensorData { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
