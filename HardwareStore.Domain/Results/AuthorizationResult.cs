using HardwareStore.Domain.Models;

namespace HardwareStore.Domain.Results;

public class AuthorizationResult : BaseResult
{
    public User User { get; set; } = null!;
}