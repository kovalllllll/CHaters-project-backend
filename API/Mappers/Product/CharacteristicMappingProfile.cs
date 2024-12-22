using API.Dtos.Product;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Product;

public class CharacteristicMappingProfile : Profile
{
    public CharacteristicMappingProfile()
    {
        CreateMap<Characteristic, CharacteristicDto>();

        CreateMap<string, Characteristic>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}