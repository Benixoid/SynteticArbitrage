namespace PriceDataFetcher.Models.ExchangeInfo
{
    //https://dapi.binance.com/dapi/v1/exchangeInfo
    // Root myDeserializedClass = JsonConvert.DeserializeObject<ExchangeRoot>(myJsonResponse);    
    public class ExchangeRoot
    {
        public string? timezone { get; set; }
        public long serverTime { get; set; }
        public List<RateLimit>? rateLimits { get; set; }
        public List<object>? exchangeFilters { get; set; }
        public List<Symbol>? symbols { get; set; }
    }
}
