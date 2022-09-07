using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TradingPlaces.Infrastucture.Repositories.Base;
using TradingPlaces.Models.Entities;

namespace TradingPlaces.Repositories
{
    public interface IStrategyRepository : IBaseRepository<StrategyDetails>
    {
        Task<List<StrategyDetails>> GetStrategiesAsync(Expression<Func<StrategyDetails, bool>> filter = null);
    }
}
