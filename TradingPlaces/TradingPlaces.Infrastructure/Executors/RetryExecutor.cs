using System;

namespace TradingPlaces.Infrastructure.Executors
{
    public static class RetryExecutor
    {
        public static T Try<T>(Func<T> onSuccess, Func<string, T> onFailure, int numberOfRetries = 10)
        {
            var tries = 0;
            string errorMessage = null;
            while (tries <= numberOfRetries)
            {
                try
                {
                    return onSuccess();
                }
                catch(Exception ex)
                {
                    tries++;
                    if (tries == numberOfRetries)
                    {
                        errorMessage = ex.Message;
                    }
                }
            }

            return onFailure($"Error after {tries} tries with message: {errorMessage}");
        }
    }
}