using Microsoft.EntityFrameworkCore.Storage;
using Net.Core;
using Net.Data.Repository;

namespace Net.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task BeginAsync()
        {
            _transaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync()
        {
            return await _appDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                if (_transaction != null)
                {
                    await RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }


        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }


        public ICRUDRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            return new CRUDRepository<TEntity>(_appDbContext);
        }
    }
}
