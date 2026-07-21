using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Contracts;
using InvoiceManagement.DTOs;
using InvoiceManagement.Entities;
using AutoMapper;

namespace InvoiceManagement.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
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
}