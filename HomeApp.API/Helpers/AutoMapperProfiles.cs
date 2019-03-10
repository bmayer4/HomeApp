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
            CreateMap<User, UserToReturnDto>();
            CreateMap<Home, HomeForDetailDto>();
            CreateMap<Home, HomeForListDto>();
            CreateMap<HomeForCreationDto, Home>();
            CreateMap<HomeForUpdateDto, Home>();
            CreateMap<PhotoForCreationDto, Photo>();
             CreateMap<Photo, PhotoForReturnDto>();
        }   
    }
}