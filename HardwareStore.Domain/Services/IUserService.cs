using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface IUserService
{
    Task<bool> CheckUserPasswordAsync(long userId, string password);
    Task<User?> GetUserAsync(string email);
    Task CreateUserAsync(User user);
}