using Dapper;
using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Repo.Data;
using BitCoinPricesWebAPI.Repo.Helper;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Repo.Repository.Base;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Data;
namespace BitCoinPricesWebAPI.Repo.Repository
{
    public class SearchRepository : RepositoryBase<object>, ISearchRepository
    {
        private readonly IConfiguration _configuration;
        private string BTCDB = String.Empty;
        private string InstanceConnectAs = String.Empty;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private DapperContext dapperDb;
        public SearchRepository(RepositoryContext repositoryContext, IConfiguration configuration,
                         DapperContext dapperContext) : base(repositoryContext)
        {
            this._configuration = configuration;
            this.BTCDB = _configuration.GetConnectionString(Enum.GetName(Common.ConnectionStrings.BTCDB));
            dapperDb = dapperContext;
        }
        public async Task<dynamic> SelectAllSources()
        {
            var procedureName = "SelectAllSources";
            var result = await QueryAsync<dynamic>(procedureName);
            return result;
        }
        
        public async Task<dynamic> GetPricesAndInsertbitstamp(string url, BitStampResponse response)
        {
            var query = string.Format("INSERT INTO bitstampPricesBX (SourceUrl,[open],timestamp,high,low,last,volume,vwap,bid,ask,open_24,percent_change_24,Mid,last_price) " +
                                                            "VALUES (@SourceUrl,@open,@timestamp,@high,@low,@last,@volume,@vwap,@bid,@ask,@open_24,@percent_change_24,@Mid,@last_price)");
            var parameters = new DynamicParameters();
            parameters.Add("Sourceurl", url, DbType.String);
            parameters.Add("timestamp", response.timestamp, DbType.Double);
            parameters.Add("open", response.open, DbType.Decimal);
            parameters.Add("high", response.high, DbType.Double);
            parameters.Add("low", response.low, DbType.Double);
            parameters.Add("last", response.last, DbType.Double);
            parameters.Add("volume", response.volume, DbType.Double);
            parameters.Add("vwap", response.vwap, DbType.Double);
            parameters.Add("bid", response.bid, DbType.Double);
            parameters.Add("ask", response.ask, DbType.Double);
            parameters.Add("open_24", response.open_24, DbType.Decimal);
            parameters.Add("percent_change_24", response.percent_change_24, DbType.Double);
            parameters.Add("Mid", response.Mid, DbType.Double);
            parameters.Add("last_price", response.last_price, DbType.Double);
            int result = await ExecuteAsync(query, parameters);
            if (result == 1)
                return "true";
            else return "false";
            
        }
        public async Task<dynamic> GetPricesAndInsertALL(string url, string response)
        {
            var query = string.Format("INSERT INTO SourcePricesBT (Sourceurl,SourceResult) VALUES (@Sourceurl,@SourceResult)");
            var parameters = new DynamicParameters();
            parameters.Add("Sourceurl", url, DbType.String);
            parameters.Add("SourceResult", response, DbType.String);
            int result = await ExecuteAsync(query, parameters);
            if (result == 1)
                return "true";
            else return "false";
        }

        public async Task<IEnumerable<dynamic>> GetHistroy()
        {
            var procedureName = "GetHistroyBTC";
            return await QueryAsync<dynamic>(procedureName);
        }
    }
}
