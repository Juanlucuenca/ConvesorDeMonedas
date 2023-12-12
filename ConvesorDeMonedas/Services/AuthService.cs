using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Migrations;
using ConvesorDeMonedas.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConvesorDeMonedas.Services
{
    public class AuthService
    {
        private readonly IRepository<User> _repository;
        private readonly IConfiguration _configuration;
        public AuthService(IRepository<User> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;

        }

        public bool RegisterUser(UserForCreationDto userForCreationDto)
        {
            bool userAlredyRegister = _repository.GetAll()
                .Any(u => u.Mail == userForCreationDto.Mail ||
                     u.UserName == userForCreationDto.UserName);


            if (userAlredyRegister) return false;


            return _repository.Insert(new User
            {
                UserName = userForCreationDto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(userForCreationDto.Password, BCrypt.Net.BCrypt.GenerateSalt()),
                Mail = userForCreationDto.Mail,
                Role = Role.User,
                ConvertionsCount = 0,
                SubscriptionId = 1
            });
        }

        public AuthResultDto validateUser(AuthDto authDto)
        {

            User? user =  _repository
                .GetAll()
                .SingleOrDefault(u => u.Mail == authDto.Mail);

            if(user is null) return new AuthResultDto { IsSuccess = false, Message = $"Incorrect Mail {authDto.Mail}" }; ;

            if (!BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password)) return new AuthResultDto { IsSuccess = false, Message = "Incorrect Password" };

            return new AuthResultDto { IsSuccess = true, Token = GenerateJwtToken(user) };

        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }
    }
}
