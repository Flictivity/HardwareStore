using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProduct(long id);
    Task<IEnumerable<Characteristic>> GetCharacteristics(long categoryId);
    Task<BaseResult> CreateProduct(Product product);
    Task<IEnumerable<Product>> GetProducts(long categoryId, bool forAdmin);
    Task<BaseResult> UpdateProduct(Product product);
}