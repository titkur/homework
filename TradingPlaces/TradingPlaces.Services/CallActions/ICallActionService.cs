using System.Threading.Tasks;
using TradingPlaces.Models.Entities;
using TradingPlaces.Models.Services.CallAction;

namespace TradingPlaces.Services.CallActions
{
    public interface ICallActionService
    {
        Task<CallActionResult> Dispatch(StrategyDetails strategy);
    }
}
