﻿using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrders(long userId);
    Task<BaseResult> CreateOrder(IEnumerable<Product?> products, long userId);
}