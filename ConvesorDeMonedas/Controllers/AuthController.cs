using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConvesorDeMonedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _Authservice;

        public AuthController(AuthService authService)
        {
            _Authservice = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(string))]
        public ActionResult Auth(AuthDto authDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {

                var result = _Authservice.validateUser(authDto);

                if (!result.IsSuccess)
                    return Unauthorized(result.Message);

                return Ok(new { token = result.Token });


            } catch (Exception ex) {

                return BadRequest(ex.Message);

            }
        }


        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public IActionResult RegisterUser([FromBody] UserForCreationDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                bool userCreated = _Authservice.RegisterUser(dto);

                if(!userCreated) { return Conflict(new { message = "Usuario ya registrado" }); }

                return Ok(new { message = "Usuario creado con exito" });

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }  
            }
    }
}
