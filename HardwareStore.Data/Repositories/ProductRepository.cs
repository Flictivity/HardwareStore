using Dapper;
using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Read.Queries;
using HardwareStore.Data.Write;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;
using HardwareStore.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HardwareStore.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;
    private readonly IImageLoadingService _imageLoadingService;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(HardwareStoreContext context, HardwareStoreReadonlyContext readonlyContext,
        IImageLoadingService imageLoadingService, ILogger<ProductRepository> logger)
    {
        _context = context;
        _readonlyContext = readonlyContext;
        _imageLoadingService = imageLoadingService;
        _logger = logger;
    }

    public async Task<Product?> GetProduct(long id)
    {
        var res = await _readonlyContext.Connection.QuerySingleOrDefaultAsync<ProductDb>(
            ProductRepositoryQueries.GetProductById,
            new {id});

        var product = EntityConverter.ConvertProduct(res);

        var characteristics = await _readonlyContext.Connection.QueryAsync<Characteristic>(
            ProductRepositoryQueries.GetExistProductCharacteristics,
            new {id});

        var category = await _readonlyContext.Connection.QuerySingleOrDefaultAsync<CategoryDb>(
            ProductRepositoryQueries.GetProductCategory,
            new {id});

        var imagesIds = await _readonlyContext.Connection
            .QueryAsync<string>(ProductRepositoryQueries.GetProductImages, new {id});

        List<Image> images = new();
        foreach (var imageId in imagesIds)
        {
            var newImage = new Image
            {
                MongoId = imageId,
                Source = await _imageLoadingService.GetImageAsBytes(imageId),
            };
            images.Add(newImage);
        }

        product.Characteristics = characteristics.ToList();
        product.Category = EntityConverter.ConvertCategory(category);
        product.Images = images;

        return res is null ? null : product;
    }

    public async Task<IEnumerable<Characteristic>> GetCharacteristics(long categoryId)
    {
        var characteristics = await _readonlyContext.Connection.QueryAsync<Characteristic>(
            ProductRepositoryQueries.GetProductCharacteristics,
            new {id = categoryId});
        return characteristics;
    }

    public async Task<BaseResult> CreateProduct(Product product)
    {
        var newProduct = new ProductDb
        {
            Name = product.Name,
            Cost = product.Cost,
            Count = product.Count,
            Code = product.Code,
            Description = product.Description,
        };

        var createdProduct = _context.Add(newProduct);
        await _context.SaveChangesAsync();
        var createdProductId = createdProduct.Entity.Id;
        _context.ChangeTracker.Clear();
        foreach (var characteristic in product.Characteristics)
        {
            var newCharacteristic = new CategoryTitleValueDb
            {
                ProductId = createdProductId,
                CategoryTitleId = characteristic.CategoryTitleId,
                Value = characteristic.Value,
            };
            _context.CategoryTitleValues.Add(newCharacteristic);
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        foreach (var image in product.Images)
        {
            var newProductImage = new ProductImageDb
            {
                ProductId = createdProductId,
                ImageSource = image.MongoId,
            };
            _context.ProductImages.Add(newProductImage);
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<Product>> GetProducts(long categoryId, bool forAdmin)
    {
        var products = new List<Product>();
        var res = !forAdmin
            ? await _readonlyContext.Connection.QueryAsync<ProductDb>(
                ProductRepositoryQueries.GetProducts, new {categoryId})
            : await _readonlyContext.Connection.QueryAsync<ProductDb>(
                ProductRepositoryQueries.GetProductsForAdmin);

        foreach (var product in res)
        {
            var convertedProduct = EntityConverter.ConvertProduct(product);
            var characteristics = await _readonlyContext.Connection.QueryAsync<Characteristic>(
                ProductRepositoryQueries.GetExistProductCharacteristics,
                new {id = product.Id});

            var category = await _readonlyContext.Connection.QuerySingleOrDefaultAsync<CategoryDb>(
                ProductRepositoryQueries.GetProductCategory,
                new {id = product.Id});

            var imagesIds = await _readonlyContext.Connection
                .QueryAsync<string>(ProductRepositoryQueries.GetProductImages, new {id = product.Id});

            List<Image> images = new();
            foreach (var imageId in imagesIds)
            {
                var newImage = new Image
                {
                    MongoId = imageId,
                    Source = await _imageLoadingService.GetImageAsBytes(imageId),
                };
                images.Add(newImage);
            }

            convertedProduct.Characteristics = characteristics.ToList();
            convertedProduct.Category = EntityConverter.ConvertCategory(category);
            convertedProduct.Images = images;

            products.Add(convertedProduct);
        }

        return products;
    }

    public async Task<BaseResult> UpdateProduct(Product product)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
        if (dbProduct is null)
        {
            _logger.LogError("Не удалось найти category_title_value для product_{Id}", product.Id);
            return new BaseResult {Success = false, Message = "Нам не удалось найти, то что вы искали"};
        }

        dbProduct.Name = product.Name;
        dbProduct.Cost = product.Cost;
        dbProduct.Count = product.Count;
        dbProduct.Code = product.Code;
        dbProduct.Description = product.Description;

        _context.Products.Update(dbProduct);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        foreach (var characteristic in product.Characteristics)
        {
            var dbCharacteristic =
                await _context.CategoryTitleValues.FirstOrDefaultAsync(x => x.Id == characteristic.Id);
            if (dbCharacteristic is null)
            {
                _logger.LogError("Не удалось найти category_title_value для product_{Id}", product.Id);
                return new BaseResult {Success = false, Message = "Нам не удалось найти, то что вы искали"};
            }

            dbCharacteristic.Value = characteristic.Value;
            _context.CategoryTitleValues.Update(dbCharacteristic);
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var oldProductImages = _context.ProductImages.Where(x => x.ProductId == product.Id).ToList();

        foreach (var image in oldProductImages)
        {
            if (product.Images.All(x => x.MongoId != image.ImageSource))
                _context.ProductImages.Remove(image);
        }

        foreach (var image in product.Images)
        {
            var oldImage = oldProductImages.FirstOrDefault(x => x.ImageSource == image.MongoId);
            if (oldImage is not null)
            {
                oldImage.ImageSource = image.MongoId;
            }
            else
            {
                _context.ProductImages.Add(new ProductImageDb
                {
                    ProductId = product.Id,
                    ImageSource = image.MongoId,
                });
            }
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        return new BaseResult {Success = true};
    }
}