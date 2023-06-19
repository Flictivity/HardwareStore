using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface IProductService
{
    Task<Product?> GetProduct(long id);
    Task<IEnumerable<Characteristic>> GetCharacteristics(long categoryId);
    Task<BaseResult> CreateProduct(Product product);
    Task<IEnumerable<Product>> GetProducts(long categoryId, bool forAdmin = false);
    Task<BaseResult> UpdateProduct(Product product);
}