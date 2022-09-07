using System.Threading.Tasks;
using TradingPlaces.Models.Services.Api;

namespace TradingPlaces.Services.Api
{
    public interface IBrokerApiService
    {
        Task<BrokerApiResponse<decimal>> GetTickerCurrentPrice(string tickerName);
        Task<BrokerApiResponse<decimal>> BuyByTickerName(string name, int quantity);
        Task<BrokerApiResponse<decimal>> SellByTickerName(string name, int quantity);
    }
}
