using AutoMapper;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, userDto>()
                .ReverseMap();
        }
    }
}
