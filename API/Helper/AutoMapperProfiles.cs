using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Extensions;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember( dest => dest.ProfileUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url
                ))
                .ForMember( dest => dest.Age, opt => opt.MapFrom(src => 
                    src.DateOfBirth.CalculateAge()
                ))
                ;
                
            CreateMap<Photo, PhotoDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();

            CreateMap<Message, MessageDto>()
                .ForMember( dest => dest.SenderPhotoUrl , opt => opt.MapFrom(src => 
                    src.Sender.Photos.FirstOrDefault( x => x.IsMain).Url
                    ))
                .ForMember( dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => 
                    src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url
                    ));
            
        }
    }
}