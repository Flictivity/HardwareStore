using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface IAuthorizationService
{
    Task<BaseResult> AuthorizeAsync(string email, string password);
    Task<BaseResult> RegisterAsync(string email, string password, string lastName, string firstName);
}