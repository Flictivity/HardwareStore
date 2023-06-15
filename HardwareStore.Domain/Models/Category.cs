namespace HardwareStore.Domain.Models;

public class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public MainCategory MainCategory { get; set; } = null!;
}