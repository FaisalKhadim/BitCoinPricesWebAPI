using Dapper;
using BitCoinPricesWebAPI.Repo.Data;
using BitCoinPricesWebAPI.Repo.Interface.Base;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
namespace BitCoinPricesWebAPI.Repo.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) =>
            RepositoryContext = repositoryContext;
        public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) =>
            !trackChanges ? await Task.Run(() => RepositoryContext.Set<T>().AsNoTracking()) : await Task.Run(() => RepositoryContext.Set<T>());
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? await Task.Run(() => RepositoryContext.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => RepositoryContext.Set<T>().Where(expression));
        public async Task CreateAsync(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Add(entity));
        public async Task UpdateAsync(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Update(entity));
        public async Task RemoveAsync(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Remove(entity));
        //Method from existing code
        #region Dapper methods
        public IDbConnection Connection => RepositoryContext.Database.GetDbConnection();
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await Connection.QueryAsync<T>(sql, param, transaction, commandType: CommandType.StoredProcedure)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: CommandType.StoredProcedure);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.QuerySingleAsync<T>(sql, param, transaction);
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.ExecuteAsync(sql, param, transaction);
        }
        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.QueryFirstOrDefaultAsync(sql, param, transaction);
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await RepositoryContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Connection.Dispose();
        }
        #endregion
    }
}