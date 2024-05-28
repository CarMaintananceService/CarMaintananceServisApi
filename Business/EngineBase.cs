using AutoMapper;
using AutoMapper.Internal.Mappers;
using Business.Shared;
using EntityFrameworkCore.Data;
using Microsoft.Extensions.Caching.Memory;

namespace Business
{


    public abstract class EngineBase : IEngine
    {
        protected readonly IMemoryCache _cache;
        protected readonly IMapper _objectMapper;
        protected readonly UserDataProvider _userDataProvider;
        protected readonly CurrentServiceProvider _serviceProvider;
        public ApplicationDbContext _dbContext { get; protected set; }


		protected int _moduleId;
        protected int _moduleTransactionTypeId;
        protected int _langId;

		public EngineBase(
            int moduleId,
            int moduleTransactionTypeId,
            IMemoryCache cache,
            IMapper objectMapper,
            UserDataProvider userDataProvider,
            CurrentServiceProvider serviceProvider
            )
        {
            _cache = cache;
            _objectMapper = objectMapper;
            _userDataProvider = userDataProvider;
            _serviceProvider = serviceProvider;
            _dbContext = (serviceProvider.GetDbContext() as ApplicationDbContext).SetUserDataProvider(userDataProvider);
        }


        public T SetDbContext<T>(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            return (T)(object)this;
        }

    }
}
