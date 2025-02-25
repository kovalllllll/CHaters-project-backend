using API.Dtos.Auth;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegistrationRequestDto, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()));

        CreateMap<LoginRequestDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}