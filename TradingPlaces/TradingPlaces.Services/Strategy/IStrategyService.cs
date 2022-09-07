using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlaces.Models.Dtos;
using TradingPlaces.Models.Entities;
using TradingPlaces.Models.Services.Ticker;

namespace TradingPlaces.Services
{
    public interface IStrategyService
    {
        Task<string> RegisterStrategyAsync(StrategyDetailsDto strategyDetailsDto);
        Task RemoveStrategyAsync(string id);
        Task<List<StrategyDetails>> GetReadyStrategiesAsync(List<TickerPrice> tickerDetailsList);
        Task<List<StrategyDetails>> GetAllStrategiesAsync();
        Task SaveChangesAsync();
    }
}
