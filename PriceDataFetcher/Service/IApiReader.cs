using SharedModels.Response;

namespace PriceDataFetcher.Service
{
    public interface IApiReader
    {
        Task<Result<string>> ReadApiDataAsync(string url);
    }
}
