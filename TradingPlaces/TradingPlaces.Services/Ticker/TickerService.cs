using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingPlaces.Infrastructure.Extensions;
using TradingPlaces.Models.Services.Ticker;
using TradingPlaces.Repositories;
using TradingPlaces.Services.Api;

namespace TradingPlaces.Services.Ticker
{
    public class TickerService : ITickerService
    {
        private readonly ITickerRepository _tickerRepository;
        private readonly IBrokerApiService _brokerApiService;
        private const int MaxDegreeOfParallelism = 1000;

        public TickerService(ITickerRepository tickerRepository, IBrokerApiService brokerApiService)
        {
            _tickerRepository = tickerRepository;
            _brokerApiService = brokerApiService;
        }

        public async Task<List<TickerPrice>> GetTickersPricesAsync()
        {
            var tickers = await _tickerRepository.GetTickersAsync();
            var concurrentDictionary = new ConcurrentDictionary<string, decimal>();
            await tickers.ParallelForEachAsync(async ticker =>
            {
                var response = await _brokerApiService.GetTickerCurrentPrice(ticker.Name);
                if (response.Success)
                {
                    concurrentDictionary.TryAdd(ticker.Name, response.Data);
                }
            }, MaxDegreeOfParallelism);

            return concurrentDictionary.Select(x => new TickerPrice
            {
                Name = x.Key,
                Price = x.Value
            }).ToList();
        }
    }
}
