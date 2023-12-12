using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConvesorDeMonedas.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConvertionHistoryController : ControllerBase
    {
        private readonly ConversionHistoryService _conversionHistoryService;
        public ConvertionHistoryController(ConversionHistoryService ConversionHistoryService)
        {
            _conversionHistoryService = ConversionHistoryService;
        }


        [HttpGet("userConvertions")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Conversion))]
        public IActionResult GetUserConvertions()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_conversionHistoryService.GetUserHistory());
        }
    }
}
