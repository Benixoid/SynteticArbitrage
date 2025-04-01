using DataStorage.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<PriceDifference> PriceDifferences { get; set; }
        public DbSet<Price> Prices { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
