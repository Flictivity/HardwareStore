using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string email);
    Task<User?> GetUserAsync(long id);
    Task<string> GetUserEncryptedPassword(long userId);
    Task CreateUserAsync(User user, string encryptedPassword);
    Task<BaseResult> ChangeUserData(User user, string encryptedPassword);
}