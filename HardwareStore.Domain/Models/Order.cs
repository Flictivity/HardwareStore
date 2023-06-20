namespace HardwareStore.Domain.Models;

public class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public long OrderSum { get; set; }

    public List<Product?> Products { get; set; } = new();
}