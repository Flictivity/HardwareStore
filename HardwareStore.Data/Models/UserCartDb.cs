namespace HardwareStore.Data.Models;

public class UserCartDb
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long UserId { get; set; }

    public long ProductCount { get; set; }

    public virtual ProductDb ProductDb { get; set; } = null!;

    public virtual UserDb UserDb { get; set; } = null!;
}
