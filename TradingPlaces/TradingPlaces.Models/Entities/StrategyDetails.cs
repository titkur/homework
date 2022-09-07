using System;
using System.ComponentModel.DataAnnotations;
using TradingPlaces.Models.Enums;
using TradingPlaces.Resources;

namespace TradingPlaces.Models.Entities
{
    public class StrategyDetails
    {
        private decimal _targetPrice;

        protected StrategyDetails() {}

        public StrategyDetails(TickerDetails ticker, BuySell action, int sharesCount, decimal priceOnCreation, decimal priceMovement)
        {
            Ticker = ticker;
            Action = action;
            SharesCount = sharesCount;
            PriceMovement = priceMovement;
            TradeTargetPrice = CalculateTradeTargetPrice(priceOnCreation);
        }

        [Key]
        public string Id { get; set; }
        public TickerDetails Ticker { get; set; }
        public BuySell Action { get; }
        public int SharesCount { get; }
        public decimal PriceMovement { get; }
        public decimal TradeTargetPrice { get; }
        public StrategyStatus Status { get; set; } = StrategyStatus.Registered;
        public decimal? ActualTradePrice { get; set; }

        public bool IsActionExecutable(decimal price)
        {
            return Action switch
            {
                BuySell.Sell => TradeTargetPrice >= price,
                BuySell.Buy => TradeTargetPrice <= price,
                _ => throw new InvalidOperationException()
            };
        }

        private decimal CalculateTradeTargetPrice(decimal priceOnCreation)
        {
            return Action switch
            {
                BuySell.Buy => Math.Round(priceOnCreation * (1 - PriceMovement / 100), 2),
                BuySell.Sell => Math.Round(priceOnCreation * (1 + PriceMovement / 100), 2),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
