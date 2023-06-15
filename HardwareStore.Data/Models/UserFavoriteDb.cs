namespace HardwareStore.Data.Models;

public class UserFavoriteDb
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long UserId { get; set; }

    public virtual ProductDb ProductDb { get; set; } = null!;

    public virtual UserDb UserDb { get; set; } = null!;
}
