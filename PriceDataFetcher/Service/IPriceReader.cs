using SharedModels.Kline;
using SharedModels.Response;

namespace PriceDataFetcher.Service
{
    public interface IPriceReader
    {
        Task<Result<IEnumerable<KlineData>?>> GetKlinesAsync(string symbol, string interval);
    }
}
