using Dapper;
using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Read.Queries;
using HardwareStore.Data.Write;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;
using HardwareStore.Domain.Services;

namespace HardwareStore.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;
    private readonly IImageLoadingService _imageLoadingService; 

    public ProductRepository(HardwareStoreContext context, HardwareStoreReadonlyContext readonlyContext, IImageLoadingService imageLoadingService)
    {
        _context = context;
        _readonlyContext = readonlyContext;
        _imageLoadingService = imageLoadingService;
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
            .QueryAsync<string>(ProductRepositoryQueries.GetProductImages, new { id});

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
}