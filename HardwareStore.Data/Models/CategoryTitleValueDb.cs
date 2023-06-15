namespace HardwareStore.Data.Models;

public class CategoryTitleValueDb
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long CategoryTitleId { get; set; }

    public string Value { get; set; } = null!;

    public virtual CategoryTitleDb CategoryTitleDb { get; set; } = null!;
}
