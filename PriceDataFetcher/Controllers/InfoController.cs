using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PriceDataFetcher.Service;
using SharedModels.Response;


namespace PriceDataFetcher.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;
        private readonly IBTCSymbolReader _symbolReader;
        public InfoController(ILogger<InfoController> logger, IBTCSymbolReader symbolReader)
        {
            _logger = logger;
            _symbolReader = symbolReader;
        }

        [HttpGet("btcusdt_quarter_symbol")]
        public async Task<ActionResult<ApiResponse<string>>> GetBtcusdt_quarterSymbol()
        {
            try
            {
                _logger.LogInformation("GetBtcusdt_quarterSymbol called");
                var symbol = await _symbolReader.GetSymbolAsync("CURRENT_QUARTER");
                if (symbol is null)
                {
                    _logger.LogError("Failed to extract btcusdt_quarter_symbol");
                    return ApiResponse<string>.ErrorResponse("Failed to extract btcusdt_quarter_symbol");
                }
                return ApiResponse<string>.SuccessResponse(symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to extract btcusdt_quarter_symbol");
                return ApiResponse<string>.ErrorResponse("Failed to extract btcusdt_quarter_symbol");
            }            
        }

        [HttpGet("btcusdt_bi_quarter_symbol")]
        public async Task<ActionResult<ApiResponse<string>>> GetBtcusdt_bi_quarterSymbol()
        {
            try
            {
                _logger.LogInformation("GetBtcusdt_bi_quarterSymbol called");            
                var symbol = await _symbolReader.GetSymbolAsync("NEXT_QUARTER");
                if (symbol is null)
                {
                    _logger.LogError("Failed to extract btcusdt_bi_quarter_symbol");
                    return ApiResponse<string>.ErrorResponse("Failed to extract btcusdt_bi_quarter_symbol");
                }            
                return ApiResponse<string>.SuccessResponse(symbol);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to extract btcusdt_bi_quarter_symbol");
                return ApiResponse<string>.ErrorResponse("Failed to extract btcusdt_bi_quarter_symbol");
            }
           
        }
    }
}
