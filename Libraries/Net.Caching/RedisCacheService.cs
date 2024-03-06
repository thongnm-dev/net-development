using Net.Caching;

namespace Net.Caching
{
    public class RedisCacheService : IStaticCacheManager
    {
        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(CacheKey key, Func<Task<T>> acquire)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(CacheKey key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        public CacheKey PrepareKey(CacheKey cacheKey, params object[] cacheKeyParameters)
        {
            throw new NotImplementedException();
        }

        public CacheKey PrepareKeyForDefaultCache(CacheKey cacheKey, params object[] cacheKeyParameters)
        {
            throw new NotImplementedException();
        }

        public CacheKey PrepareKeyForShortTermCache(CacheKey cacheKey, params object[] cacheKeyParameters)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(CacheKey cacheKey, params object[] cacheKeyParameters)
        {
            throw new NotImplementedException();
        }
    }
}
