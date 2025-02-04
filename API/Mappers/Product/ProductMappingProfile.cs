using API.Dtos.Product;
using AutoMapper;
using DAL.Entities;

namespace API.Mappers.Product;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<DAL.Entities.Product, ProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(image => new ImageDto
            {
                Id = image.Id.ToString(),
                Name = image.Name,
                ContentType = image.ContentType,
                Url = image.Path
            }).ToList()));
        
        CreateMap<ProductRequestDto, DAL.Entities.Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductCharacteristics, opt => opt.Ignore());
        
        CreateMap<Image, ImageDto>();
    }
}