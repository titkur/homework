namespace TradingPlaces.Models.Services.Api
{
    public class BrokerApiResponse<T>
    {
        public T Data { get; set; } = default;
        public string ErrorMessage { get; set; } = null;
        public bool Success { get { return ErrorMessage == null; } }
    }
}
