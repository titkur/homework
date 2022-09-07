using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TradingPlaces.Infrastucture.Repositories.Base;
using TradingPlaces.Models.Entities;
using TradingPlaces.Repositories.Context;

namespace TradingPlaces.Repositories
{
    public class StrategyRepository : BaseRepository<StrategyDetails>, IStrategyRepository
    {
        private readonly EfDbContext _dbContext;

        public StrategyRepository(EfDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StrategyDetails>> GetStrategiesAsync(Expression<Func<StrategyDetails, bool>> filter)
        {
            return await  _dbContext.Strategies.Where(filter).ToListAsync();
        }
    }
}
