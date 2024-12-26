using API.Dtos.Product;
using AutoMapper;

namespace API.Mappers.Product;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<DAL.Entities.Product, ProductDto>();
        
        CreateMap<ProductRequestDto, DAL.Entities.Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.Images, opt => opt.Ignore()) 
            .ForMember(dest => dest.ProductCharacteristics, opt => opt.Ignore());
    }
}