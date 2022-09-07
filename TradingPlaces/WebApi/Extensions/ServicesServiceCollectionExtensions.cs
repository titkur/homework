using Microsoft.Extensions.DependencyInjection;
using Reutberg;
using System;
using System.Collections.Generic;
using TradingPlaces.Resources;
using TradingPlaces.Services;
using TradingPlaces.Services.Api;
using TradingPlaces.Services.CallActions;
using TradingPlaces.Services.Factories.CallAction;
using TradingPlaces.Services.Ticker;

namespace TradingPlaces.WebApi.Extensions
{
    public static class ServicesServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStrategyService, StrategyService>();
            services.AddTransient<IBrokerApiService, BrokerApiService>();
            services.AddTransient<CallBuyActionService>();
            services.AddTransient<CallSellActionService>();
            services.AddTransient<ICallActionServiceFactory, CallActionServiceFactory>(ctx =>
            {
                var factories = new Dictionary<BuySell, Func<ICallActionService>>()
                {
                    [BuySell.Buy] = () => ctx.GetService<CallBuyActionService>(),
                    [BuySell.Sell] = () => ctx.GetService<CallSellActionService>(),
                };
                return new CallActionServiceFactory(factories);
            });
            services.AddTransient<ITickerService, TickerService>();
            services.AddTransient<IReutbergService, ReutbergService>();
        }
    }
}
