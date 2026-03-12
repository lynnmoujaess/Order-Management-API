using OrderManagement_Api.Common.Exceptions;
using OrderManagement_Api.DTOs.Orders;
using OrderManagement_Api.DTOs.Products;
using OrderManagement_Api.Models;
using OrderManagement_Api.Repositories.Interfaces;
using OrderManagement_Api.Services.Interfaces;

namespace OrderManagement_Api.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
       var products = await _productRepository.GetAllAsync();
        return products.Select(MapToResponseDto);

    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return null;
        return MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) throw new NotFoundException($"Product with id {id} not found");

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToResponseDto(product);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) throw new NotFoundException($"Product with id {id} not found");

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();
    }
    
    private static ProductResponseDto MapToResponseDto(Product product) => new ProductResponseDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Stock = product.Stock
    };
}