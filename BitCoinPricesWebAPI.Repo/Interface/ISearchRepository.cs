using BitCoinPricesWebAPI.Core.Models;
namespace BitCoinPricesWebAPI.Repo.Interface
{
    public interface ISearchRepository
    {
        Task<dynamic> GetPricesAndInsertALL(string url, string response);
        Task<dynamic> GetPricesAndInsertbitstamp(string url, BitStampResponse response);
        Task<dynamic> SelectAllSources();
        Task<IEnumerable<dynamic>> GetHistroy();
    }
}
