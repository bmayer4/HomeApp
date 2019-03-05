using AutoMapper;
using HomeApp.API.Dtos;
using HomeApp.API.Models;

namespace HomeApp.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            // source to dest
            CreateMap<UserForRegisterDto, User>();
        }   
    }
}