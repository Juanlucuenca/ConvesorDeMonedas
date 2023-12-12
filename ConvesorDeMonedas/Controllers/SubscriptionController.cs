using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConvesorDeMonedas;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService;
    public SubscriptionController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }


    [HttpPost("{subscriptionId}")]
    public IActionResult Subscribe(int subscriptionId)
    {
        try
        {
            return Ok(_subscriptionService.SetUserSubscription(subscriptionId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(ICollection<Subscription>))]
    public IActionResult getSubscriptions()
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        try
        {
            return Ok(_subscriptionService.GetAll());

        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{subId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200, Type = typeof(Subscription))]
    public IActionResult getSubscriptionsById(int subId)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        try
        {
            return Ok(_subscriptionService.GetById(subId));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
