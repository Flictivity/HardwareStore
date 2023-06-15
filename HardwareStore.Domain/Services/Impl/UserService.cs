using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class UserService : IUserService
{
    private readonly IEncryptionService _encryptionService;
    private readonly IUserRepository _userRepository;

    public UserService(IEncryptionService encryptionService, IUserRepository userRepository)
    {
        _encryptionService = encryptionService;
        _userRepository = userRepository;
    }

    public async Task<bool> CheckUserPasswordAsync(long userId, string password)
    {
        var encryptedPassword = await _encryptionService.EncryptStringAsync(password);
        var storedUserPassword = await _userRepository.GetUserEncryptedPassword(userId);

        return encryptedPassword.Equals(storedUserPassword);
    }

    public Task<User?> GetUserAsync(string email)
    {
        return _userRepository.GetUserAsync(email);
    }

    public async Task CreateUserAsync(User user)
    {
        var encryptedPassword = await _encryptionService.EncryptStringAsync(user.Password);
        await _userRepository.CreateUserAsync(user, encryptedPassword);
    }
}