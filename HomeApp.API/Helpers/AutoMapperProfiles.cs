using System.Linq;
using AutoMapper;
using HomeApp.API.Data;
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
            CreateMap<Home, HomeForDetailDto>()
                .ForMember(dest => dest.CoverUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsCover).Url);
                })
                 .ForMember(dest => dest.DaysOnMarket, opt => {
                    opt.MapFrom(src => src.DateAdded.CalculateDaysOnMarket());
                });
            CreateMap<Home, HomeForListDto>()
                .ForMember(dest => dest.CoverUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsCover).Url);
                })
                .ForMember(dest => dest.NumberOfPhotos, opt => {
                    opt.MapFrom(src => src.Photos.Count);
                })
                .ForMember(dest => dest.DaysOnMarket, opt => {
                    opt.MapFrom(src => src.DateAdded.CalculateDaysOnMarket());
                })
                 .ForMember(dest => dest.FavUserIds, opt => {
                    opt.MapFrom(src => src.Favorites.Select(f => f.UserId));
                });
            CreateMap<HomeForCreationDto, Home>();
            CreateMap<HomeForUpdateDto, Home>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
        }   
    }
}