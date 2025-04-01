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
    public class PriceDifferenceController : ControllerBase
    {
        private readonly IPriceService _priceService;
        private readonly ILogger<PriceDifferenceController> _logger;
        public PriceDifferenceController(IPriceService priceService, ILogger<PriceDifferenceController> logger)
        {            
            _priceService = priceService;
            _logger = logger;
        }
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PriceDifferenceDTO>>> PostAsync([FromBody] PriceDifInput data)
        {
            try
            {
                //data.Symbol = null;
                var createdEntity = await _priceService.SavePriceDifferenceAsync(data);
                return Ok(ApiResponse<PriceDifferenceDTO>.SuccessResponse(createdEntity));
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(ApiResponse<PriceDifferenceDTO>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Internal server error");
                return Ok(ApiResponse<PriceDifferenceDTO>.ErrorResponse("Internal server error"));
            }
        }
        
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
