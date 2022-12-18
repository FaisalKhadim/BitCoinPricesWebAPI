namespace BitCoinPricesWebAPI.Repo.Interface
{
    public interface ILoggerManagerRepository
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
