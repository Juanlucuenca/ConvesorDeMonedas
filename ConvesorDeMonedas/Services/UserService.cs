using AutoMapper;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Models;
using System.Drawing;

namespace ConvesorDeMonedas.Services
{
    public class UserService
    {
        private readonly IRepository<User> _repository;
        private readonly SessionService _sessionService;

        public UserService(IRepository<User> repository, SessionService sessionService)
        {
            _repository = repository;
            _sessionService = sessionService;
        }


        public User GetById(int userId)
        {
            if (!userExist(userId))
                throw new Exception($"El usuario con Id {userId} no existe");

            return _repository.GetById(userId);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public bool Update(User userUpdated)
        {

            User userForUpdate = _repository.GetById(userUpdated.Id);

            userForUpdate.UserName = userUpdated.UserName;
            userForUpdate.Mail = userUpdated.Mail;
            userForUpdate.Role = userUpdated.Role;
            userForUpdate.ConvertionsCount = userUpdated.ConvertionsCount;
            userForUpdate.SubscriptionId = userUpdated.SubscriptionId;

            return _repository.Update(userForUpdate);

        }

        public bool Delete(int userId)
        {
            if (!userExist(userId)) return false;

            return _repository.Delete(userId);
        }

        public bool userExist(int userId)
        {
            return _repository.Exist(userId);
        }

        public UserProfileDto GetUserProfile()
        {
            int userId = _sessionService.GetUserId();

            User? user = _repository
                .GetAllIncluding(x => x.Subscription)
                .FirstOrDefault(x => x.Id == userId);

            return new UserProfileDto
            {
                Name = user.UserName,
                Mail = user.Mail,
                Subscription = user.Subscription.Name,
                Convertions = user.ConvertionsCount,
                Role = user.Role.ToString(),    
            };
        }

    }
}
