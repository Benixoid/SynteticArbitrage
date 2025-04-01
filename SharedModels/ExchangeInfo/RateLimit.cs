namespace PriceDataFetcher.Models.ExchangeInfo
{
    public class RateLimit
    {
        public string? rateLimitType { get; set; }
        public string? interval { get; set; }
        public int intervalNum { get; set; }
        public int limit { get; set; }
    }


}
