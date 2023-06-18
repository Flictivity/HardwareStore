namespace HardwareStore.Domain.Models;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long Cost { get; set; }

    public int Count { get; set; }

    public long Code { get; set; }

    public string? Description { get; set; }

    public List<Characteristic> Characteristics { get; set; } = new();
    
    public Category Category { get; set; } = new();

    public List<Image> Images { get; set; } = new();
}