namespace BitCoinPricesWebAPI.Core.Dtos
{
    public class BitStampResponseDTO
    {
        public double timestamp { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double last { get; set; }
        public double volume { get; set; }
        public double vwap { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
        public double open_24 { get; set; }
        public double percent_change_24 { get; set; }
        public double Mid { get; set; }
    }
}
