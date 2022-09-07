using System.Threading.Tasks;

namespace TradingPlaces.Infrastucture.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> FindAsync(string id);
        Task<T> AddAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task CommitAsync();
    }
}
