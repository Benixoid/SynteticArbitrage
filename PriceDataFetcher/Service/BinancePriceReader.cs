using Newtonsoft.Json;
using SharedModels.Kline;
using SharedModels.Response;
using System.Net.Http;

namespace PriceDataFetcher.Service
{
    public class BinancePriceReader : IPriceReader
    {        
        private readonly ILogger<BinancePriceReader> _logger;
        private readonly IApiReader _apiReader;
        private readonly string _baseUrl = "https://dapi.binance.com/dapi/v1/klines";
        public BinancePriceReader(ILogger<BinancePriceReader> logger, IApiReader apiReader)
        {
            _logger = logger;
            _apiReader = apiReader;
        }
        public async Task<Result<IEnumerable<KlineData>?>> GetKlinesAsync(string symbol, string interval)
        {
            var url = $"{_baseUrl}?symbol={symbol}&interval={interval}";            
            var apiReslt = await _apiReader.ReadApiDataAsync(url);
            if (apiReslt.IsSuccess)
            {
                var klines = JsonConvert.DeserializeObject<List<List<object>>>(apiReslt.Value);
                if (klines is null)
                {
                    _logger.LogError("Failed to deserialize klines data");
                    return Result<IEnumerable<KlineData>?>.Failure("DESERIALIZE_ERROR", "Failed to deserialize klines data");
                }
                var result = new List<KlineData>();
                foreach (var item in klines)
                {
                    result.Add(KlineData.ConvertToKlineData(item));
                }
                return Result<IEnumerable<KlineData>?>.Success(result);
            }
            return Result<IEnumerable<KlineData>?>.Failure(apiReslt.ErrorCode, apiReslt.ErrorMessage);
        }

        
    }
}
