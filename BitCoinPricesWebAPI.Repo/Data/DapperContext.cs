using Dapper;
using BitCoinPricesWebAPI.Repo.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace BitCoinPricesWebAPI.Repo.Data
{
    public class DapperContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(Enum.GetName(Common.ConnectionStrings.BTCDB));
        }
        private IDbConnection Connection
            => new SqlConnection(_connectionString);
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await Connection.QueryAsync<T>(sql, param, transaction,commandType: CommandType.StoredProcedure)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.QuerySingleAsync<T>(sql, param, transaction);
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await Connection.ExecuteAsync(sql, param, transaction);
        }
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
