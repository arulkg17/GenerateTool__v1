using Microsoft.EntityFrameworkCore;
using <Your Namespace>.Contracts;
using <Your Namespace>.DTOs;
using <Your Namespace>.Entities;
using AutoMapper;

namespace <Your Namespace>.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(entities);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        return _mapper.Map<ProductDto>(entity);
    }

    public async Task<ProductDto> AddAsync(ProductDto dto)
    {
        var entity = _mapper.Map<ProductEntity>(dto);

        var created = await _repository.AddAsync(entity);

        return _mapper.Map<ProductDto>(created);
    }

    public async Task<bool> UpdateAsync(ProductDto dto)
    {
        var entity = _mapper.Map<ProductEntity>(dto);
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}