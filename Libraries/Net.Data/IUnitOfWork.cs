using Microsoft.EntityFrameworkCore.Storage;
using Net.Core;
using Net.Data.Repository;

namespace Net.Data
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Initialize transaction this intance UnitOfWork
        /// </summary>
        /// <returns></returns>
        public Task BeginAsync();

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <returns>Transaction</returns>
        public Task<IDbContextTransaction> CreateTransactionAsync();

        /// <summary>
        /// Commit transaction after all process successfully
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync();


        /// <summary>
        /// Rollback data if proccess has been exception
        /// </summary>
        /// <returns></returns>
        public Task RollbackAsync();

        /// <summary>
        /// Initialize repository to use annual transaction
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ICRUDRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    }
}
