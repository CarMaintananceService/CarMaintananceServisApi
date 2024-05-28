using EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CurrentServiceProvider
    {
        DbContext _dbContext;
        IDbContextFactory<DbContext> _dbContextFactory;
        IServiceProvider _serviceProvider;
        
        //Dictionary<Type, object> _cachedObjects = new Dictionary<Type, object>();

        public CurrentServiceProvider() {

        }

        

        public void Set(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public DbContext GetNewDbContext()
        {
            if (_dbContextFactory == null)
                _dbContextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<DbContext>>();

            return _dbContextFactory.CreateDbContext();
        }

        public DbContext GetDbContext()
        {
            if(_dbContext == null)
                _dbContext = _serviceProvider.GetRequiredService<DbContext>();

            return _dbContext;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();

            //if (!_cachedObjects.ContainsKey(typeof(T)))
            //    _cachedObjects.Add(typeof(T), _serviceProvider.GetRequiredService<T>());

            //return (T)_cachedObjects[typeof(T)];
        }

    }
}
