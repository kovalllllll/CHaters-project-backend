using API.Dtos.Product;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Product;

public class ProductCharacteristicMappingProfile : Profile
{
    public ProductCharacteristicMappingProfile()
    {
        CreateMap<ProductCharacteristic, ProductCharacteristicDto>()
            .ForMember(dest => dest.Characteristic, opt => opt.MapFrom(src => src.CharacteristicId));
        CreateMap<ProductCharacteristicRequestDto, ProductCharacteristic>()
            .ForMember(dest => dest.CharacteristicId, opt => opt.MapFrom(src => src.CharacteristicId));
        CreateMap<ProductCharacteristicUpdateDto, ProductCharacteristic>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.CharacteristicId, opt => opt.MapFrom(src => src.CharacteristicId));
        
    }
}