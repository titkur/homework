using TradingPlaces.Resources;
using TradingPlaces.Services.CallActions;

namespace TradingPlaces.Services.Factories.CallAction
{
    public interface ICallActionServiceFactory
    {
        ICallActionService GetCallActionService(BuySell action);
    }
}
