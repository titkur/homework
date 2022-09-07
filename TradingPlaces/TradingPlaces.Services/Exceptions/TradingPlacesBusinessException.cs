using System;

namespace TradingPlaces.Services.Exceptions
{
    public class TradingPlacesBusinessException : Exception
    {
        public TradingPlacesBusinessException() : base() { }
        public TradingPlacesBusinessException(string message) : base(message) { }
        public TradingPlacesBusinessException(string message, Exception inner) : base(message, inner) { }
    }
}
