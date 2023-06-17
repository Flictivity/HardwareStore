using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> GetProduct(long id)
    {
        return await _productRepository.GetProduct(id);
    }

    public async Task<IEnumerable<Characteristic>> GetCharacteristics(long categoryId)
    {
        return await _productRepository.GetCharacteristics(categoryId);
    }

    public async Task<BaseResult> CreateProduct(Product product)
    {
        return await _productRepository.CreateProduct(product);
    }
}