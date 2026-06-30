using AutoMapper;
using <Your Namespace>.Entities;
using <Your Namespace>.DTOs;

namespace <Your Namespace>.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
    }
}
