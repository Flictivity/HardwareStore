using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class UserProductsService : IUserProductsService
{
    private readonly IUserProductRepository _userProductRepository;

    public UserProductsService(IUserProductRepository userProductRepository)
    {
        _userProductRepository = userProductRepository;
    }

    public async Task<BaseResult> AddToCartAsync(long productId, long userId)
    {
        return await _userProductRepository.AddToCartAsync(productId, userId);
    }

    public async Task<BaseResult> DeleteFromCart(long productId, long userId)
    {
        return await _userProductRepository.DeleteFromCart(productId,userId);
    }

    public async Task<IEnumerable<Product?>> GetUserCartProducts(long userId)
    {
        return await _userProductRepository.GetUserCartProducts(userId);
    }

    public async Task<BaseResult> AddToFavoriteAsync(long productId, long userId)
    {
        return await _userProductRepository.AddToFavoriteAsync(productId,userId);
    }

    public async Task<BaseResult> DeleteFromFavorite(long productId, long userId)
    {
        return await _userProductRepository.DeleteFromFavorite(productId, userId);
    }

    public async Task<IEnumerable<Product?>> GetUserFavoriteProducts(long userId)
    {
        return await _userProductRepository.GetUserFavoriteProducts(userId);
    }
}