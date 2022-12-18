using BitCoinPricesWebAPI.Repo.Data;
using BitCoinPricesWebAPI.Repo.Helper;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Repo.Repository.Base;
using Microsoft.Extensions.Configuration;
using NLog;
namespace BitCoinPricesWebAPI.Repo.Repository
{
    public class ErrorLogRepository : RepositoryBase<object>, IErrorLogRepository
    {
        private readonly IConfiguration _configuration;
        private string BTCDB = String.Empty;
        private string InstanceConnectAs = String.Empty;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private DapperContext dapperDb;
        public ErrorLogRepository(RepositoryContext repositoryContext, IConfiguration configuration,
                         DapperContext dapperContext) : base(repositoryContext)
        {
            this._configuration = configuration;
            this.BTCDB = _configuration.GetConnectionString(Enum.GetName(Common.ConnectionStrings.BTCDB));
            dapperDb = dapperContext;
        }
    }
}
