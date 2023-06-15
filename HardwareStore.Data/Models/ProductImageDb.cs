namespace HardwareStore.Data.Models;

public class ProductImageDb
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public string ImageSource { get; set; } = null!;

    public virtual ProductDb ProductDb { get; set; } = null!;
}
