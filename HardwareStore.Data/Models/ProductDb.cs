namespace HardwareStore.Data.Models;

public class ProductDb
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long Cost { get; set; }

    public int Count { get; set; }

    public long Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CategoryTitleValueDb> CategoryTitleValues { get; set; } = new List<CategoryTitleValueDb>();

    public virtual ICollection<OrderProductDb> OrderProducts { get; set; } = new List<OrderProductDb>();

    public virtual ICollection<ProductImageDb> ProductImages { get; set; } = new List<ProductImageDb>();

    public virtual ICollection<UserCartDb> UserCarts { get; set; } = new List<UserCartDb>();

    public virtual ICollection<UserFavoriteDb> UserFavorites { get; set; } = new List<UserFavoriteDb>();
}
