using Asp.Versioning;
using DataStorage.Models;
using DataStorage.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DataStorage.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;
        private readonly ILogger<PriceController> _logger;
        public PriceController(IPriceService priceService, ILogger<PriceController> logger)
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
        public async Task<ActionResult> PostAsync([FromBody] PriceDifInput data)
        {
            try
            {
                data.Symbol = null;
                var createdEntity = await _priceService.SavePriceDifferenceAsync(data);
                return Ok(createdEntity);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Internal server error");
                return StatusCode(500, "Internal server error");
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
