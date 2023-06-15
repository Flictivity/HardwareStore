using HardwareStore.Domain.Enums;
using HardwareStore.Domain.ErrorMessages;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserService _userService;
    private readonly IEncryptionService _encryptionService;

    public AuthorizationService(IUserService userService, IEncryptionService encryptionService)
    {
        _userService = userService;
        _encryptionService = encryptionService;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(string email, string password)
    {
        var user = await _userService.GetUserAsync(email);

        if (user is null)
            return new AuthorizationResult {Success = false, Message = UserErrorMessages.NotFound};


        var isPasswordValid = await _userService.CheckUserPasswordAsync(user.Id, password);
        return isPasswordValid
            ? new AuthorizationResult {Success = true, User = user}
            : new AuthorizationResult {Success = false, Message = UserErrorMessages.WrongPassword};
    }

    public async Task<BaseResult> RegisterAsync(string email, string password, string lastName, string firstName)
    {
        var existingUser = await _userService.GetUserAsync(email);

        if (existingUser is not null)
            return new BaseResult{Success = false, Message = UserErrorMessages.UserAlreadyExists};

        var user = new User
        {
            Email = email,
            Password = password,
            LastName = lastName,
            FirstName = firstName,
            Role = Roles.User
        };

        await _userService.CreateUserAsync(user);

        return new BaseResult {Success = true};
    }
}