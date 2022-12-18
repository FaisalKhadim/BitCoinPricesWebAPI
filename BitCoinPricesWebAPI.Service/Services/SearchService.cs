using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using System.Net.Http.Headers;
namespace BitCoinPricesWebAPI.Service.Services
{
    public class SearchService : ISearchService
    {
        ISearchRepository isearchRepository;
        IConfiguration configuration;
        private string BTCDB = String.Empty;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        public SearchService(ISearchRepository _isearchRepository, IConfiguration _configuration)
        {
            this.isearchRepository = _isearchRepository;
            this.configuration = _configuration;
        }
        public async Task<dynamic> GetPricesAndInsertALL(string SourceEndPoint)
        {

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.BaseAddress = new Uri(SourceEndPoint);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //Sending request to find web api REST service resource user info using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync(client.BaseAddress);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    _logger.Info($"Fetched Data from soource");
                    return await isearchRepository.GetPricesAndInsertALL(SourceEndPoint, ObjResponse);
                        
                    }
                    else
                    {

                    _logger.Error($"Error in Http response {nameof(GetPricesAndInsertALL)}");
                        return "";
                    }
                }
            
        }
        public async Task<dynamic> GetPricesAndInsertbitstamp(string SourceEndPoint)
        {
            using (var clientHandler = new HttpClientHandler())
            {
                // to Bypass SSL policy  if  needed this is additional not required
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    //Passing service base url  
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.BaseAddress = new Uri(SourceEndPoint);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                      
                    HttpResponseMessage Res = await client.GetAsync(client.BaseAddress);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                        
                        var APIResponse = JsonConvert.DeserializeObject<BitStampResponse>(ObjResponse);
                        _logger.Info($"Fetched Data from soource");
                        return await isearchRepository.GetPricesAndInsertbitstamp(SourceEndPoint, APIResponse);
                   
                    }
                    else
                    {
                        _logger.Error($"Error in Http response {nameof(GetPricesAndInsertALL)}");
                        return "";
                    }
                }
            }
        }
        public async Task<dynamic> SelectAllSources()
        {
            return await isearchRepository.SelectAllSources();
        }
        public async Task<IEnumerable<dynamic>> GetHistroy()
        {
            IEnumerable<dynamic> HistData = await isearchRepository.GetHistroy();
            return HistData;
        }
    }
}
