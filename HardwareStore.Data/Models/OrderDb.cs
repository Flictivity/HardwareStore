namespace HardwareStore.Data.Models;

public class OrderDb
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public long OrderSum { get; set; }

    public virtual ICollection<OrderProductDb> OrderProducts { get; set; } = new List<OrderProductDb>();

    public virtual UserDb UserDb { get; set; } = null!;
}
