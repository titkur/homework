using Reutberg;
using System;
using System.Threading.Tasks;
using TradingPlaces.Infrastructure.Executors;
using TradingPlaces.Models.Services.Api;

namespace TradingPlaces.Services.Api
{
    public class BrokerApiService : IBrokerApiService
    {
        private readonly IReutbergService _reutbergService;

        public BrokerApiService(IReutbergService reutbergService)
        {
            _reutbergService = reutbergService;
        }

        public Task<BrokerApiResponse<decimal>> GetTickerCurrentPrice(string tickerName)
        {
            return RetryExecutor.Try(
                () =>
                {
                    var result = _reutbergService.GetQuote(tickerName);
                    return Task.FromResult(new BrokerApiResponse<decimal> {Data = result});
                },
                errorMessage => Task.FromResult(new BrokerApiResponse<decimal> {ErrorMessage = errorMessage}));
        }

        public Task<BrokerApiResponse<decimal>> BuyByTickerName(string name, int quantity)
        {
            return RetryExecutor.Try(() =>
            {
                var result = _reutbergService.Buy(name, quantity);
                return Task.FromResult(new BrokerApiResponse<decimal> {Data = result});
            }, errorMessage => Task.FromResult(new BrokerApiResponse<decimal> {ErrorMessage = errorMessage}));
        }

        public Task<BrokerApiResponse<decimal>> SellByTickerName(string name, int quantity)
        {
            return RetryExecutor.Try(() =>
            {
                var result = _reutbergService.Sell(name, quantity);
                return Task.FromResult(new BrokerApiResponse<decimal> {Data = result});
            }, errorMessage => Task.FromResult(new BrokerApiResponse<decimal> {ErrorMessage = errorMessage}));
        }
    }
}