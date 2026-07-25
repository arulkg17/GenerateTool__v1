using AutoMapper;
using InvoiceManagement.Entities;
using InvoiceManagement.DTOs;

namespace InvoiceManagement.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryEntity, CategoryDto>()
            .ReverseMap();
    }
}