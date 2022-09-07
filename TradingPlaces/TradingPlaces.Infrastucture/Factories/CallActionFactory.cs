using Microsoft.Extensions.DependencyInjection;
using System;
using TradingPlaces.Resources;
using TradingPlaces.Services.CallActions;

namespace TradingPlaces.Infrastucture.Factories
{
    public class CallActionFactory : ICallActionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CallActionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICallActionService GetCallActionService(BuySell action)
        {
            return action switch
            {
                BuySell.Buy => _serviceProvider.GetRequiredService<CallBuyActionService>(),
                BuySell.Sell => _serviceProvider.GetRequiredService<CallSellActionService>(),
                _ => throw new InvalidOperationException(),
            };
        } 
    }
}
