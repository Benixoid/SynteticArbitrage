using SharedModels.Response;

namespace PriceDataFetcher.Service
{
    public class ApiReader : IApiReader
    {
        private readonly ILogger<ApiReader> _logger;

        public ApiReader(ILogger<ApiReader> logger)
        {
            _logger = logger;
        }

        public async Task<Result<string>> ReadApiDataAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to get data from api({url}). Status code: {response.StatusCode}");
                        return Result<string>.Failure(response.StatusCode.ToString(), response.RequestMessage!.ToString());
                    }
                    var data = await response.Content.ReadAsStringAsync();
                    return Result<string>.Success(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting data from ({url}): {ex.Message}");
                return Result<string>.Failure("API_ERROR", ex.Message);
            }            
        }        
    }
}
