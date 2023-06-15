namespace HardwareStore.Data.Models;

public class UserDb
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<OrderDb> Orders { get; set; } = new List<OrderDb>();

    public virtual ICollection<UserCartDb> UserCarts { get; set; } = new List<UserCartDb>();

    public virtual ICollection<UserFavoriteDb> UserFavorites { get; set; } = new List<UserFavoriteDb>();
}
