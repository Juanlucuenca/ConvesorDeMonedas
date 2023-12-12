using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Services
{
    public class SubscriptionService
    {
        private readonly IRepository<Subscription> _repository;
        private readonly UserService _userService;
        private readonly SessionService _sessionService;
        public SubscriptionService(IRepository<Subscription> repository, UserService userService, SessionService sessionService)
        {
            _repository = repository;
            _userService = userService;
            _sessionService = sessionService;
        }

        public bool SetUserSubscription(int subscriptionId)
        {
            int userId = _sessionService.GetUserId();
            User user = _userService.GetById(userId);
            Subscription subscription  = _repository.GetById(subscriptionId);

            if(!_repository.Exist(subscription.Id))
                throw new Exception($"Subscripcion con el id {subscription.Id} no encontrado");

            if(user.SubscriptionId != 1)
            {
                throw new Exception($"El usuario ya posee una subscripcion");
            }

            user.SubscriptionId = subscription.Id;
            return _userService.Update(user);

        }

        public Subscription GetById(int subId)
        {
            return _repository.GetById(subId);
        }

        public bool exists(int subId)
        {
            return _repository.Exist(subId);
        }

        public IEnumerable<Subscription> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
