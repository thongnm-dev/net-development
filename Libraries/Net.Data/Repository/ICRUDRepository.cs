using Net.Core;

namespace Net.Data.Repository
{
    public interface ICRUDRepository<TEntity> where TEntity : BaseEntity
    {
        public Task CreateAsync(TEntity entity, bool publishEvent = true);

        public Task UpdateAsync(TEntity entity, bool publishEvent = true);

        public Task DeleteAsync(TEntity entity, bool publishEvent = true);

    }
}
