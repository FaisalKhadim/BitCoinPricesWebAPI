using BitCoinPricesWebAPI.Repo.Helper;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using NLog;
namespace BitCoinPricesWebAPI.Service.Services
{
    public class ErrorLogService : IErrorLogService
    {
        IErrorLogRepository iErrorLogRepository;
        IConfiguration configuration;
        private string BTCDB = String.Empty;
        private string InstanceConnectAs = String.Empty;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        public ErrorLogService(IErrorLogRepository _iErrorLogRepository, IConfiguration _configuration)
        {
            this.iErrorLogRepository = _iErrorLogRepository;
            this.configuration = _configuration;
            this.BTCDB = _configuration.GetConnectionString(Enum.GetName(Common.ConnectionStrings.BTCDB));
        }
    }
}
