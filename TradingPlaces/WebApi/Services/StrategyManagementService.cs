using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TradingPlaces.Models.Enums;
using TradingPlaces.Resources;
using TradingPlaces.Services;
using TradingPlaces.Services.Factories.CallAction;
using TradingPlaces.Services.Ticker;
using TradingPlaces.Infrastructure.Extensions;

namespace TradingPlaces.WebApi.Services
{
    internal class StrategyManagementService : TradingPlacesBackgroundServiceBase, IStrategyManagementService
    {
        private const int TickFrequencyMilliseconds = 1000;
        private readonly ILogger<StrategyManagementService> _logger;
        private readonly ITickerService _tickerService;
        private readonly IStrategyService _strategyService;
        private readonly ICallActionServiceFactory _callActionServiceFactory;
        private const int MaxDegreeOfParallelism = 1000;

        public StrategyManagementService(ILogger<StrategyManagementService> logger, ITickerService tickerService, IStrategyService strategyService, ICallActionServiceFactory callActionServiceFactory)
            : base(TimeSpan.FromMilliseconds(TickFrequencyMilliseconds), logger)
        {
            _logger = logger;
            _tickerService = tickerService;
            _strategyService = strategyService;
            _callActionServiceFactory = callActionServiceFactory;
        }

        protected override async Task CheckStrategies()
        {
            try
            {
                var currentTickerPrices = await _tickerService.GetTickersPricesAsync();
                if (!currentTickerPrices.Any()) return;

                var strategies = await _strategyService.GetReadyStrategiesAsync(currentTickerPrices);
                if (!strategies.Any()) return;

                await strategies.ParallelForEachAsync(async strategy =>
                {
                    var callActionService = _callActionServiceFactory.GetCallActionService(strategy.Action);
                    var actionResult = await callActionService.Dispatch(strategy);
                    if (actionResult.SucessfullyDispatched)
                    {
                        strategy.Status = StrategyStatus.Finished;
                        strategy.ActualTradePrice = actionResult.DispatchedActionPrice;
                    }
                }, MaxDegreeOfParallelism);

                await _strategyService.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Job Failed with message: {ex.Message}");
            }


            await Task.CompletedTask;
        }
    }
}
