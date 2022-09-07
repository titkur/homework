using System.Threading.Tasks;
using TradingPlaces.Models.Entities;
using TradingPlaces.Models.Services.CallAction;
using TradingPlaces.Services.Api;

namespace TradingPlaces.Services.CallActions
{
    public class CallSellActionService : ICallActionService
    {
        private readonly IBrokerApiService _brokerApiService;

        public CallSellActionService(IBrokerApiService brokerApiService)
        {
            _brokerApiService = brokerApiService;
        }

        public async Task<CallActionResult> Dispatch(StrategyDetails strategy)
        {
            var brokerBuyResponse = await _brokerApiService.SellByTickerName(strategy.Ticker.Name, strategy.SharesCount);
            return new CallActionResult
            {
                DispatchedActionPrice = brokerBuyResponse.Data,
                SucessfullyDispatched = brokerBuyResponse.Success
            };
        }
    }
}
