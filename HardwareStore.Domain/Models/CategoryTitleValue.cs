namespace HardwareStore.Domain.Models;

public class CategoryTitleValue
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long CategoryTitleId { get; set; }

    public string Value { get; set; } = null!;

    public virtual CategoryTitle CategoryTitleDb { get; set; } = null!;

    public virtual Product ProductDb { get; set; } = null!;
}