namespace HardwareStore.Domain.Models;

public class Characteristic
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public long Id { get; set; }
    public long CategoryTitleId { get; set; }
}