using InvoiceManagement.Contracts;
using InvoiceManagement.DTOs;
using InvoiceManagement.Entities;
using AutoMapper;

namespace InvoiceManagement.Services;

public class CategoryServiceEF : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryServiceEF(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(entities);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task<CategoryDto> AddAsync(CategoryDto dto)
    {
        var entity = _mapper.Map<CategoryEntity>(dto);

        var created = await _repository.AddAsync(entity);

        return _mapper.Map<CategoryDto>(created);
    }
    public async Task<bool> UpdateAsync(CategoryDto dto)
    {
        var entity = _mapper.Map<CategoryEntity>(dto);

        return await _repository.UpdateAsync(entity);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
   
   public async Task<PagedResultDto<CategoryDto>> GetAllPagedAsync(CategorySearchDto request)
   {
     var result = await _repository.GetAllPagedAsync(request);

     return new PagedResultDto<CategoryDto>
     {
        Items = _mapper.Map<IEnumerable<CategoryDto>>(result.Items),
        TotalRecords = result.TotalRecords,
        PageNumber = result.PageNumber,
        PageSize = result.PageSize
     };
   }
}