using Microsoft.EntityFrameworkCore;
using Net.Core;
using Net.Caching;
using Net.Data.Extensions;

namespace Net.Data.Repository
{
    internal class ReadRepository<TEntity> : BaseRepository<TEntity>, IReadRepository<TEntity> where TEntity : BaseEntity
    {
        #region Variable
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public ReadRepository(AppDbContext appDbContext, IStaticCacheManager staticCacheManager) : base(appDbContext)
        {
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Properties
        public IQueryable<TEntity> Table => Entities.AsNoTracking();
        #endregion

        #region Method


        public async Task<TEntity> QueryFirtOrDefault(
                                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? predicate = null
                                              , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true)
        {
            async Task<TEntity> fncFirstOrDefault()
            {
                var query = IncludeDetedFilter(Table, includeDeleted);

                query = predicate != null ? predicate(query) : query;

                return await query.FirstOrDefaultAsync();
            }

            if (getCacheKey == null)
            {
                return await fncFirstOrDefault();
            }

            var cacheKey = getCacheKey(_staticCacheManager);

            return await _staticCacheManager.GetAsync(cacheKey, fncFirstOrDefault);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(
                                                      Func<IQueryable<TEntity>, IQueryable<TEntity>>? predicate = null
                                                    , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true)
        {
            async Task<IEnumerable<TEntity>> fncGetEntitiesAsync()
            {
                var query = IncludeDetedFilter(Table, includeDeleted);

                query = predicate != null ? predicate(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(fncGetEntitiesAsync, getCacheKey);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
              Func<IQueryable<TEntity>, IQueryable<TEntity>>? func
            , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false)
        {
            async Task<IEnumerable<TEntity>> fncGetAllAsync()
            {
                var query = IncludeDetedFilter(Table, includeDeleted);

                query = func != null ? func(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(fncGetAllAsync, getCacheKey);
        }

        public async Task<IPagedList<TEntity>> GetAllPagedAsync(
              Func<IQueryable<TEntity>, IQueryable<TEntity>>? func
            , int pageNumber = 1, int pageSize = 20, bool includeDeleted = false)
        {
            var query = IncludeDetedFilter(Table, includeDeleted);

            query = func != null ? func(query) : query;

            return await query.ToPageListAsync(pageNumber, pageSize);
        }

        public async Task<TEntity> GetByIdAsync(int? id, Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true)
        {
            if (!id.HasValue || id == 0) return null;

            async Task<TEntity> _getEntityAsync()
            {
                return await IncludeDetedFilter(Table, includeDeleted).FirstOrDefaultAsync(entity => entity.Id == Convert.ToInt32(id.Value));
            }

            if (getCacheKey == null)
            {
                return await _getEntityAsync();
            }

            var cacheKey = getCacheKey(_staticCacheManager);

            return await _staticCacheManager.GetAsync(cacheKey, _getEntityAsync);
        }

        #endregion

        #region Private

        private IQueryable<TEntity> IncludeDetedFilter(IQueryable<TEntity> query, in bool includeDeleted)
        {
            if (includeDeleted) return query;

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletedEntity)) == null)
                return query;

            return query.OfType<ISoftDeletedEntity>().Where(entry => entry.IsDeleted).OfType<TEntity>();
        }

        private async Task<IEnumerable<TEntity>> GetEntitiesAsync(Func<Task<IEnumerable<TEntity>>> fncGetAllAsync, Func<IStaticCacheManager, CacheKey>? getCacheKey = null)
        {
            if (getCacheKey == null)
                return await fncGetAllAsync();

            var cacheKey = getCacheKey(_staticCacheManager);

            return await _staticCacheManager.GetAsync(cacheKey, fncGetAllAsync);
        }
        #endregion
    }
}
