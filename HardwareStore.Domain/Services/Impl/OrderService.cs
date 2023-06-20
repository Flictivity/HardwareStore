using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetOrders(long userId)
    {
        return await _orderRepository.GetOrders(userId);
    }

    public async Task<BaseResult> CreateOrder(IEnumerable<Product?> products, long userId)
    {
        return await _orderRepository.CreateOrder(products, userId);
    }
}