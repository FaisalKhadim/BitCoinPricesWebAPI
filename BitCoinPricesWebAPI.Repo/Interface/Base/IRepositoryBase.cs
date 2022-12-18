using System.Data;
using System.Linq.Expressions;
namespace BitCoinPricesWebAPI.Repo.Interface.Base;
public interface IRepositoryBase<T>
{
    IDbConnection Connection { get; }
    Task<IQueryable<T>> FindAllAsync(bool trackChanges);
    Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
}
