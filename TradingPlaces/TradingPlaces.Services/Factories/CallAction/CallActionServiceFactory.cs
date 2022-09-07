using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TradingPlaces.Resources;
using TradingPlaces.Services.CallActions;

namespace TradingPlaces.Services.Factories.CallAction
{
    public class CallActionServiceFactory : ICallActionServiceFactory
    {
        private readonly Dictionary<BuySell, Func<ICallActionService>> _actions;

        public CallActionServiceFactory(Dictionary<BuySell, Func<ICallActionService>> actions)
        {
            _actions = actions;
        }

        public ICallActionService GetCallActionService(BuySell action)
        {
            if (!_actions.TryGetValue(action, out var factory) || factory is null)
                throw new ArgumentOutOfRangeException(nameof(action), $"Type '{action}' is not registered");
            return factory();
        } 
    }
}
