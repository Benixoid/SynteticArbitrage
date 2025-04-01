using Newtonsoft.Json;
using PriceDataFetcher.Controllers;
using PriceDataFetcher.Models.ExchangeInfo;
using System;

namespace PriceDataFetcher.Service
{
    public class BinanceSymbolInfoReader : ISymbolInfoReader
    {
        private const string url = "https://dapi.binance.com/dapi/v1/exchangeInfo";
        private readonly ILogger<InfoController> _logger;

        public BinanceSymbolInfoReader(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        public async Task<ExchangeRoot?> ReadData()
        {            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    if (response is not null)
                    {
                        ExchangeRoot? myDeserializedClass = JsonConvert.DeserializeObject<ExchangeRoot>(response);
                        return myDeserializedClass;
                    }
                    else
                    {
                        _logger.LogError("No data from BINANCE received, response is null");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read data from Binance");                    
                }                
                return null;
            }
        }
    }
}
