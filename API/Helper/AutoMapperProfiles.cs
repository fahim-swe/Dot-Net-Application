using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest=> dest.Age, opt=>opt.MapFrom(src => src.GetAge()))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain == true).Url));


            // CreateMap<AppUser, MemberDto>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.GetAge()))
            //     .ForMember(dest=> dest.PhotoUrl, opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<Photos, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<PaginationFilter, PaginationFilter>();
        }
    }
}