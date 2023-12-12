using AutoMapper;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConvesorDeMonedas;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly SessionService _sessionService;
    private readonly SubscriptionService _subscriptionService;

    public UserController(UserService userService, IMapper mapper, SessionService sessionService, SubscriptionService subscriptionService)
    {
        _userService = userService;
        _mapper = mapper;
        _sessionService = sessionService;
        _subscriptionService = subscriptionService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(User))]
    public IActionResult GetById(int userId)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(_mapper.Map<userDto>(_userService.GetById(userId)));
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(ICollection<User>))]
    public IActionResult GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(_mapper.Map<List<userDto>>(_userService.GetAll()));
    }

    [HttpPut]
    [ProducesResponseType(404)]
    [ProducesResponseType(403)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Update([FromBody] userDto userUpdated)
    {

        Role roleClaim = _sessionService.GetUserRol();
        if (roleClaim == Role.User)
           return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            if (!_subscriptionService.exists(userUpdated.SubscriptionId)) return NotFound(new { message = "La subscripcion no existe" });
            if (!_userService.userExist(userUpdated.Id)) return NotFound(new { message = "El usuario no existe" });


            User userMap = _mapper.Map<User>(userUpdated);
            bool canUpdate = _userService.Update(userMap);
            if (canUpdate) return Ok(new { message = "Usuario Actulizado" });
            return BadRequest(new { message = "Algo salio mal" });


        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("profile")]
    [ProducesResponseType(200, Type = typeof(UserProfileDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetProfile()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            UserProfileDto userProfile = _userService.GetUserProfile();
            return Ok(userProfile);

        } catch (Exception ex) {
           return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    public IActionResult Delete([FromQuery] int userId)
    {

        Role roleClaim = _sessionService.GetUserRol();
        if (roleClaim == Role.User)
           return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            bool canDeleted = _userService.Delete(userId);
            if(canDeleted) return Ok(new { message = "Usuario eliminado" });
            return Conflict();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
