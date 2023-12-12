using AutoMapper;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace ConvesorDeMonedas.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class currencyController : ControllerBase
    {
        private readonly RequestSerivce _requestService;
        private readonly CurrencyService _currencyService;
        private readonly SessionService _sessionService;
        
        public currencyController(
                RequestSerivce requestSerivce, 
                CurrencyService currencyService,
                SessionService sessionService
         )
        {
            _requestService = requestSerivce;
            _currencyService = currencyService;
            _sessionService = sessionService;
        }

        [HttpGet("{currencyId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type =  typeof(Currency))]
        public IActionResult GetById(int currencyId) {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                Currency currency = _currencyService.GetCurrencyById(currencyId);
                return Ok(currency);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ICollection<Currency>))]
        public IActionResult GetAll()
        {

            if(!ModelState.IsValid) 
                return BadRequest();

            try
            {
                return Ok(_currencyService.GetAllCurrencies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }    
        }

        [HttpPut]
        public IActionResult Update([FromBody] Currency currency)
        {
            Role roleClaim = _sessionService.GetUserRol();
            if(roleClaim == Role.User)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _currencyService.UpdateCurrency(currency);

                return Ok(new { message = "Moneda actualizada" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromQuery] int currencyId)
        {
            Role roleClaim = _sessionService.GetUserRol();
            if (roleClaim == Role.User)
                return Forbid();

            try
            {
                _currencyService.DeleteCurrency(currencyId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPost("createCurrency")]
        [ProducesResponseType(200)]
        public IActionResult Create(CurrencyDto currencyModel) 
        {
            Role roleClaim = _sessionService.GetUserRol();
            if (roleClaim == Role.User)
                return Forbid();

            Currency currency = new Currency() {
                Name = currencyModel.CurrencyName,
                Ic = currencyModel.CurrencyIc,
                Symbol = currencyModel.Symbol
            };

            try
            {
                _currencyService.CreateCurrency(currency);
                return NoContent();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("convert")]
        public IActionResult ConvertCurrency(ConvertCurrencyDto currencyConvertInfo)
        {
            try
            {
                double result = _currencyService.ConvertCurrency(currencyConvertInfo);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
