using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TradingPlaces.Infrastucture.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<T> FindAsync(string id) => _dbContext.FindAsync<T>(id);

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var result = _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
