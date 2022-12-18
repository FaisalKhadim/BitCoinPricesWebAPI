namespace BitCoinPricesWebAPI.Core.Responses
{
    public class Response<T> 
    {
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
