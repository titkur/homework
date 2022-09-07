using System.Threading.Tasks;
using TradingPlaces.Models.Entities;
using TradingPlaces.Models.Services.CallAction;
using TradingPlaces.Services.Api;

namespace TradingPlaces.Services.CallActions
{
    public class CallBuyActionService : ICallActionService
    {
        private readonly IBrokerApiService _brokerApiService;

        public CallBuyActionService(IBrokerApiService brokerApiService)
        {
            _brokerApiService = brokerApiService;
        }

        public async Task<CallActionResult> Dispatch(StrategyDetails strategy)
        {
            var brokerBuyResponse = await _brokerApiService.BuyByTickerName(strategy.Ticker.Name, strategy.SharesCount);
            return new CallActionResult
            {
                DispatchedActionPrice = brokerBuyResponse.Data,
                SucessfullyDispatched = brokerBuyResponse.Success
            };
        }
    }
}
