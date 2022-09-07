using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlaces.Infrastucture.Repositories.Base;
using TradingPlaces.Models.Entities;

namespace TradingPlaces.Repositories
{
    public interface ITickerRepository : IBaseRepository<TickerDetails>
    {
        Task<TickerDetails> GetTickerByNameAsync(string name);
        Task<List<TickerDetails>> GetTickersAsync();
    }
}
