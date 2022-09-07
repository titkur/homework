using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingPlaces.Models.Dtos;
using TradingPlaces.Models.Entities;
using TradingPlaces.Models.Enums;
using TradingPlaces.Models.Services.Ticker;
using TradingPlaces.Repositories;
using TradingPlaces.Services.Api;
using TradingPlaces.Services.Exceptions;

namespace TradingPlaces.Services
{
    public class StrategyService : IStrategyService
    {
        private readonly IBrokerApiService _brokerApiService;
        private readonly ITickerRepository _tickerRepository;
        private readonly IStrategyRepository _strategyRepository;

        public StrategyService(IBrokerApiService brokerApiService, ITickerRepository tickerRepository, IStrategyRepository strategyRepository)
        {
            _brokerApiService = brokerApiService;
            _tickerRepository = tickerRepository;
            _strategyRepository = strategyRepository;
        }

        public async Task<string> RegisterStrategyAsync(StrategyDetailsDto strategyDetailsDto)
        {
            var apiResponse = await _brokerApiService.GetTickerCurrentPrice(strategyDetailsDto.Ticker);

            if (!apiResponse.Success) throw new TradingPlacesBusinessException(apiResponse.ErrorMessage);

            var ticker = await _tickerRepository.GetTickerByNameAsync(strategyDetailsDto.Ticker) ?? new TickerDetails { Name = strategyDetailsDto.Ticker };
            var strategy = await _strategyRepository.AddAsync(new StrategyDetails(ticker, strategyDetailsDto.Instruction, strategyDetailsDto.Quantity, apiResponse.Data, strategyDetailsDto.PriceMovement));

            if (strategy == null) throw new TradingPlacesBusinessException("Unable to create strategy");

            return strategy.Id;
        }

        public async Task RemoveStrategyAsync(string id)
        {
            var strategy = await _strategyRepository.FindAsync(id);

            if (strategy == null) throw new TradingPlacesBusinessException($"Strategy with id: {id} not found");

            await _strategyRepository.RemoveAsync(strategy);
        }

        public async Task<List<StrategyDetails>> GetReadyStrategiesAsync(List<TickerPrice> tickerDetailsList)
        {
            return await _strategyRepository.GetStrategiesAsync(
                x => x.Status == StrategyStatus.Registered && tickerDetailsList.Any(td => td.Name == x.Ticker.Name && x.IsActionExecutable(td.Price)));      
        }

        public async Task SaveChangesAsync()
        {
            await _strategyRepository.SaveChangesAsync();
        }

        public async Task<List<StrategyDetails>> GetAllStrategiesAsync()
        {
            return await _strategyRepository.GetStrategiesAsync();
        }
    }
}
