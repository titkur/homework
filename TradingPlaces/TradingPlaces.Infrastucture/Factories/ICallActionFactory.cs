using TradingPlaces.Resources;
using TradingPlaces.Services.CallActions;

namespace TradingPlaces.Infrastucture.Factories
{
    public interface ICallActionFactory
    {
        ICallActionService GetCallActionService(BuySell action);
    }
}
