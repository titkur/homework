using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TradingPlaces.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction, int maxDegreeOfParallelism)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDegreeOfParallelism);
            var tasks = source.Select(async item =>
            {
                await throttler.WaitAsync();
                try
                {
                    await Task.Run(async () => await asyncAction(item));
                }
                finally
                {
                    throttler.Release();
                }
            });
            await Task.WhenAll(tasks);
        }
    }
}
