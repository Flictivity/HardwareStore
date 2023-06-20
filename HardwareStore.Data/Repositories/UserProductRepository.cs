using Dapper;
using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Read.Queries;
using HardwareStore.Data.Write;
using HardwareStore.Domain.ErrorMessages;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;
using Microsoft.Extensions.Logging;

namespace HardwareStore.Data.Repositories;

public class UserProductRepository : IUserProductRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<UserProductRepository> _logger;

    public UserProductRepository(HardwareStoreContext context, IProductRepository productRepository,
        HardwareStoreReadonlyContext readonlyContext, ILogger<UserProductRepository> logger)
    {
        _context = context;
        _productRepository = productRepository;
        _readonlyContext = readonlyContext;
        _logger = logger;
    }

    public async Task<BaseResult> AddToCartAsync(long productId, long userId)
    {
        var newUserCartProduct = new UserCartDb
        {
            ProductId = productId,
            UserId = userId,
            ProductCount = 1,
        };

        _context.UserCarts.Add(newUserCartProduct);
        await _context.SaveChangesAsync();

        return new BaseResult {Success = true};
    }

    public async Task<BaseResult> DeleteFromCart(long productId, long userId)
    {
        var cartProduct = _context.UserCarts.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        if (cartProduct is null)
            return new BaseResult {Success = false, Message = "Не удалось удалить товар из корзины"};
        _context.UserCarts.Remove(cartProduct);
        await _context.SaveChangesAsync();
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<Product?>> GetUserCartProducts(long userId)
    {
        var productIds =
            await _readonlyContext.Connection.QueryAsync<long>(UserCartRepositoryQueries.GetProductsForCart,
                new {id = userId});

        var products = new List<Product?>();

        foreach (var productId in productIds)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product is null)
            {
                _logger.LogError("Не удалось найти товар с Id{ID}", productId);
                products.Add(product);
            }

            products.Add(product);
        }

        return products;
    }

    public async Task<BaseResult> AddToFavoriteAsync(long productId, long userId)
    {
        if (_context.UserFavorites.Any(x => x.ProductId == productId && x.UserId == userId))
        {
            return new BaseResult {Success = false, Message = ProductErrorMessages.CartErrorMessage};
        }

        var newUserFavoriteProduct = new UserFavoriteDb
        {
            ProductId = productId,
            UserId = userId
        };

        _context.UserFavorites.Add(newUserFavoriteProduct);
        await _context.SaveChangesAsync();

        return new BaseResult {Success = true};
    }

    public async Task<BaseResult> DeleteFromFavorite(long productId, long userId)
    {
        var favoriteProduct = _context.UserFavorites.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        if (favoriteProduct is null)
            return new BaseResult {Success = false, Message = "Не удалось удалить товар из избранных"};
        _context.UserFavorites.Remove(favoriteProduct);
        await _context.SaveChangesAsync();
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<Product?>> GetUserFavoriteProducts(long userId)
    {
        var productIds =
            await _readonlyContext.Connection.QueryAsync<long>(UserCartRepositoryQueries.GetProductsForFavorite,
                new {id = userId});

        var products = new List<Product?>();

        foreach (var productId in productIds)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product is null)
            {
                _logger.LogError("Не удалось найти товар с Id{ID}", productId);
                products.Add(product);
            }

            products.Add(product);
        }

        return products;
    }
}