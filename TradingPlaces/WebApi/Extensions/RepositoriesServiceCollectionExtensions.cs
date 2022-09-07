using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradingPlaces.Repositories;
using TradingPlaces.Repositories.Context;

namespace TradingPlaces.WebApi.Extensions
{
    public static class RepositoriesServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStrategyRepository, StrategyRepository>();
            services.AddTransient<ITickerRepository, TickerRepository>();
            services.AddDbContext<EfDbContext>(options => { options.UseInMemoryDatabase("db"); }, ServiceLifetime.Singleton);
        }
    }
}
