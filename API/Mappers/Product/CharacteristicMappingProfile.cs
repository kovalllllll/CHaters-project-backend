using API.Dtos.Product;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Product;

public class CharacteristicMappingProfile : Profile
{
    public CharacteristicMappingProfile()
    {
        CreateMap<Characteristic, CharacteristicDto>();

        CreateMap<CharacteristicRequestDto, Characteristic>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<CharacteristicUpdateDto, Characteristic>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        
    }
}