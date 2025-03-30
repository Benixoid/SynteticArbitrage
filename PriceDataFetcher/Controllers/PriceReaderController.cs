using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PriceDataFetcher.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]    
    [ApiVersion("1.0")]
    public class PriceReaderController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value" + id;
        }
    }
}
