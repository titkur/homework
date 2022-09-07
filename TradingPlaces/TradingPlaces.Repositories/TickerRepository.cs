using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlaces.Repositories.Context;
using TradingPlaces.Models.Entities;
using TradingPlaces.Infrastucture.Repositories.Base;

namespace TradingPlaces.Repositories
{
    public class TickerRepository : BaseRepository<TickerDetails>, ITickerRepository
    {
        private readonly EfDbContext _dbContext;

        public TickerRepository(EfDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TickerDetails> GetTickerByNameAsync(string name)
        {
            return _dbContext.Tickers.FirstOrDefaultAsync(t => t.Name == name);
        }

        public Task<List<TickerDetails>> GetTickersAsync()
        {
            return _dbContext.Tickers.ToListAsync();
        }
    }
}
