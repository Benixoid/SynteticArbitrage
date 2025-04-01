using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PriceDataFetcher.Service;
using SharedModels.Kline;
using SharedModels.Response;

namespace PriceDataFetcher.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]    
    [ApiVersion("1.0")]
    public class Last10PricesController : ControllerBase
    {
        private readonly ILogger<Last10PricesController> _logger;
        private readonly IPriceReader _priceReader;

        public Last10PricesController(ILogger<Last10PricesController> logger, IPriceReader priceReader)
        {
            _logger = logger;
            _priceReader = priceReader;
        }
        
        [HttpGet("{symbol}")]
        public async Task<ApiResponse<IEnumerable<KlineData>>> Get(string symbol, string interval)
        {
            try
            {
                var result = await _priceReader.GetKlinesAsync(symbol, interval);
                if (result.IsSuccess)
                {
                    return ApiResponse<IEnumerable<KlineData>>.SuccessResponse(result.Value!.TakeLast(10));
                }
                else
                {
                    _logger.LogError($"Failed to read price for symbol: {symbol}. Error: {result.ErrorMessage}");
                    return ApiResponse<IEnumerable<KlineData>>.ErrorResponse(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to read price for symbol: {symbol}");
                return ApiResponse<IEnumerable<KlineData>>.ErrorResponse(ex.Message);
            }
        }
    }
}
