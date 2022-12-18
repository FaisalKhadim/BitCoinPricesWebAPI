namespace BitCoinPricesWebAPI.Service.Interfaces;
public interface ILoggerManagerService
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
}
