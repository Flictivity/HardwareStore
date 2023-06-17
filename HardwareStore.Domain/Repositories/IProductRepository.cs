using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProduct(long id);
    Task<IEnumerable<Characteristic>> GetCharacteristics(long categoryId);
    Task<BaseResult> CreateProduct(Product product);
}