using Microsoft.EntityFrameworkCore;
using TradingPlaces.Models.Entities;

namespace TradingPlaces.Repositories.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<StrategyDetails> Strategies { get; set; }
        public DbSet<TickerDetails> Tickers { get; set; }

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StrategyDetails>()
                .HasOne(p => p.Ticker);
        }
    }
}
