using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Shared.Entities;
using Core;
using Core.Entities;

namespace EntityFrameworkCore.Data
{
	public class RepositoryBase<TEntity>: IRepository<TEntity>
		where TEntity : class, new()
	{
		protected readonly DbContext db;
		protected readonly DbSet<TEntity> dbSet;


		public DbContext GetDbContext() { 
			return db;  
		}

		#region RepositoryBase (Constructor)
		public RepositoryBase(DbContext dbContext)
		{
			this.db = dbContext;
			this.dbSet = db.Set<TEntity>();
		}
		#endregion

		#region GetAsync
		public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await dbSet.FirstOrDefaultAsync(predicate);
		}
		public async Task<TEntity?> GetAsyncNoTracking(Expression<Func<TEntity, bool>> predicate)
		{
			return await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
		}
		
		#endregion

		#region GetAll
		public IQueryable<TEntity> GetAll()
		{
			return dbSet;
		}

	
		//public IQueryable<TEntity> GetAllByCompany(int CompanyId) 
		//{
		//    return dbSet.Where(e => (e as BaseCompanyModel).CompanyId == CompanyId);
		//}
		public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.Where(predicate);
		}
		#endregion

		#region GetCount
		public Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.Where(predicate).CountAsync();
		}
		#endregion

		#region Add
		public void Add(TEntity entity)
		{
			dbSet.Add(entity);
		}
		#endregion

		//public void GetOsman(TEntity entity)
		//{
		//    string osman = "";
		//}

		#region Update
		public void Update(TEntity entity)
		{
			dbSet.Attach(entity);
			db.Entry(entity).State = EntityState.Modified;
		}
		#endregion

		#region Delete
		public async Task Delete(Expression<Func<TEntity, bool>> predicate)
		{
			var entity = await GetAsync(predicate);

			Delete(entity);
		}

		public void Delete(TEntity entity)
		{
			db.Entry(entity).State = EntityState.Deleted;
		}

		public void DeleteRange(IEnumerable<TEntity> entities)
		{
			entities.All(e => {
				db.Entry(e).State = EntityState.Deleted;
				return true;
			});
		}

		public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
		{
			GetAll(predicate).ToArray().All(e =>
			{
				db.Entry(e).State = EntityState.Deleted;
				return true;
			});
		}
		#endregion

		#region AnyAsync
		public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await dbSet.AnyAsync(predicate);
		}
		#endregion

		#region SaveChangeAsync
		public Task SaveChangeAsync()
		{
			return db.SaveChangesAsync();
		}
		#endregion

		#region Dispose

		// Flag: Has Dispose already been called?
		bool disposed = false;

		// Public implementation of Dispose pattern callable by consumers.
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Protected implementation of Dispose pattern.
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				// Free any other managed objects here.
				db.Dispose();
			}

			// Free any unmanaged objects here.

			disposed = true;
		}



		~RepositoryBase()
		{
			Dispose(false);
		}

		#endregion

	}
}
