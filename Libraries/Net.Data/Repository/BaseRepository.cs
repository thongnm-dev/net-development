using Microsoft.EntityFrameworkCore;
using Net.Core;

namespace Net.Data.Repository
{
    internal class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Field
        private readonly AppDbContext _appDbContext;

        #endregion

        #region Common
        protected DbSet<TEntity> Entities => _appDbContext.Set<TEntity>();

        #endregion

        #region Ctor
        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }
        #endregion
    }
}
