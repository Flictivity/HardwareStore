using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface IUserProductRepository
{
    Task<BaseResult> AddToCartAsync(long productId, long userId);
    Task<BaseResult> DeleteFromCart(long productId, long userId);
    Task<IEnumerable<Product?>> GetUserCartProducts(long userId);
    Task<BaseResult> AddToFavoriteAsync(long productId, long userId);
    Task<BaseResult> DeleteFromFavorite(long productId, long userId);
    Task<IEnumerable<Product?>> GetUserFavoriteProducts(long userId);
}