using API.Dtos.Product;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Characteristics;

public class CharacteristicMappingProfile : Profile
{
    public CharacteristicMappingProfile()
    {
        CreateMap<CharacteristicDto, Characteristic>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Characteristic, CharacteristicDto>();

        // Додати цей мапінг
        CreateMap<string, Characteristic>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
