using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Shared.Entities
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsyncNoTracking(Expression<Func<TEntity, bool>> predicate);

        
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        
        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        Task Delete(Expression<Func<TEntity, bool>> predicate);
        void DeleteRange(IEnumerable<TEntity> entities);
        void DeleteRange(Expression<Func<TEntity, bool>> predicate);


        void Delete(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task SaveChangeAsync();
        DbContext GetDbContext();

        //void GetOsman(TEntity entity);

    }
}
