namespace HardwareStore.Data.Models;

public class OrderProductDb
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public long Count { get; set; }

    public virtual OrderDb OrderDb { get; set; } = null!;

    public virtual ProductDb ProductDb { get; set; } = null!;
}
