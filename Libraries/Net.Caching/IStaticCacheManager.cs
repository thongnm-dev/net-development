using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Caching
{
    public interface IStaticCacheManager
    {
        Task<T> GetAsync<T>(CacheKey key, Func<Task<T>> acquire);

        Task<T> GetAsync<T>(CacheKey key, Func<T> acquire);

        Task RemoveAsync(CacheKey cacheKey, params object[] cacheKeyParameters);

        Task ClearAsync();

        #region Cache key

        /// <summary>
        /// Create a copy of cache key and fills it by passed parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="cacheKeyParameters">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKey(CacheKey cacheKey, params object[] cacheKeyParameters);

        /// <summary>
        /// Create a copy of cache key using the default cache time and fills it by passed parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="cacheKeyParameters">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKeyForDefaultCache(CacheKey cacheKey, params object[] cacheKeyParameters);

        /// <summary>
        /// Create a copy of cache key using the short cache time and fills it by passed parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="cacheKeyParameters">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKeyForShortTermCache(CacheKey cacheKey, params object[] cacheKeyParameters);

        #endregion
    }
}
