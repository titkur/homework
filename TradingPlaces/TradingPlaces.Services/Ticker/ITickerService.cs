using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlaces.Models.Services.Ticker;

namespace TradingPlaces.Services.Ticker
{
    public interface ITickerService
    {
        Task<List<TickerPrice>> GetTickersPricesAsync();
    }
}
