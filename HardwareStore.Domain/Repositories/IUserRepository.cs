using HardwareStore.Domain.Models;

namespace HardwareStore.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string email);
    Task<string> GetUserEncryptedPassword(long userId);
    Task CreateUserAsync(User user, string encryptedPassword);
}