using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Write;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;
    private readonly IProductRepository _productRepository;

    public OrderRepository(HardwareStoreContext context, HardwareStoreReadonlyContext readonlyContext, IProductRepository productRepository)
    {
        _context = context;
        _readonlyContext = readonlyContext;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Order>> GetOrders(long userId)
    {
        var orders = _context.Orders.Where(x => x.UserId == userId);
        
        var convertedOrders = new List<Order>();
        
        foreach (var order in orders)
        {
            var products = new List<Product?>();
            var convertedOrder = EntityConverter.ConvertOrder(order);
            var orderProducts = _context.OrderProducts.Where(x => x.OrderId == order.Id);

            foreach (var product in orderProducts)
            {
                products.Add(await _productRepository.GetProduct(product.ProductId));
            }

            convertedOrder.Products = products;
            convertedOrders.Add(convertedOrder);
        }

        return convertedOrders;
    }

    public async Task<BaseResult> CreateOrder(IEnumerable<Product?> products, long userId)
    {
        var enumerable = products as Product[] ?? products.ToArray();
        var newOrder = new OrderDb
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            OrderSum = enumerable.ToList().Sum(x => x.Cost),
        };
        var createdOrder = _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync();

        foreach (var product in enumerable)
        {
            if(product is null)
                continue;
            var newOrderProduct = new OrderProductDb
            {
                OrderId = createdOrder.Entity.Id,
                ProductId = product.Id,
                Count = 1,
            };
            _context.Add(newOrderProduct);
        }

        await _context.SaveChangesAsync();

        return new BaseResult{Success = true};
    }
}