using Net.Core;
using Net.Caching;

namespace Net.Data.Repository
{
    public interface IReadRepository<TEntity> where TEntity : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        #endregion

        #region Method

        Task<TEntity> QueryFirtOrDefault(
                Func<IQueryable<TEntity>, IQueryable<TEntity>>? predicate = null
              , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true);

        Task<IEnumerable<TEntity>> QueryAsync(
                Func<IQueryable<TEntity>, IQueryable<TEntity>>? predicate = null
              , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true);

        Task<TEntity> GetByIdAsync(int? id, Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = true);

        Task<IEnumerable<TEntity>> GetAllAsync(
              Func<IQueryable<TEntity>, IQueryable<TEntity>>? func
            , Func<IStaticCacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

        Task<IPagedList<TEntity>> GetAllPagedAsync(
              Func<IQueryable<TEntity>, IQueryable<TEntity>>? func
            , int pageNumber = 1, int pageSize = 20, bool includeDelted = false);
        #endregion
    }
}
