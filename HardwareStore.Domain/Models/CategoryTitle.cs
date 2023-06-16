namespace HardwareStore.Domain.Models;

public class CategoryTitle
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    public Category Category { get; set; } = null!;
}