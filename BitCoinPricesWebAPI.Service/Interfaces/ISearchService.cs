namespace BitCoinPricesWebAPI.Service.Interfaces
{
    public interface ISearchService
    {
        Task<dynamic> GetPricesAndInsertALL(string SourceEndPoint);
        Task<dynamic> GetPricesAndInsertbitstamp(string url);
        Task<dynamic> SelectAllSources();
        Task<IEnumerable<dynamic>> GetHistroy();
    }
}
