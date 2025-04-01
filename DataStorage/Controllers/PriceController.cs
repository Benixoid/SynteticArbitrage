using Asp.Versioning;
using DataStorage.Models;
using DataStorage.Models.DTO;
using DataStorage.Services;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Response;

namespace DataStorage.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class Price123Controller : ControllerBase
    {
        private readonly IPriceService _priceService;
        private readonly ILogger<Price123Controller> _logger;
        public Price123Controller(IPriceService priceService, ILogger<Price123Controller> logger)
        {            
            _priceService = priceService;
            _logger = logger;
        }
       
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PriceDTO>>> PostAsync([FromBody] PriceInput data)
        {
            try
            {
                //data.Symbol = null;
                var createdEntity = await _priceService.SavePriceAsync(data);
                return Ok(ApiResponse<PriceDTO>.SuccessResponse(createdEntity));
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(ApiResponse<PriceDTO>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Internal server error");
                return Ok(ApiResponse<PriceDTO>.ErrorResponse("Internal server error"));
            }
        }        
    }
}
