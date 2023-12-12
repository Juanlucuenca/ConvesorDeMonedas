using System.Data;
using System.Diagnostics;
using AutoMapper;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Services
{
    public class RequestSerivce
    {
        private readonly UserService _userService;
        private readonly SubscriptionService _subscriptionService;
        private readonly SessionService _sessionService;
        public RequestSerivce(UserService userService, SessionService sessionService, SubscriptionService subscriptionService)
        {

            _userService = userService;
            _sessionService = sessionService;
            _subscriptionService = subscriptionService;

        }

        public void IncrementRequestCount()
        {
            int userId = _sessionService.GetUserId();
            User user = _userService.GetById(userId);
            int subConverts = _subscriptionService.GetById(user.SubscriptionId).AmountOfConvertions;

            if (user is null)
                throw new Exception("Error al validar usuario");

            bool canRequest = user.ConvertionsCount <= subConverts;

            if(!canRequest) {
                throw new Exception("Excediste el limite de peticiones");
            }
            
            user.ConvertionsCount++;
            _userService.Update(user);
        }
    }
}
