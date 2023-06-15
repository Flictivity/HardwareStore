using HardwareStore.Domain.Enums;

namespace HardwareStore.Domain.Models;

public class User
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public Roles Role { get; set; }
}