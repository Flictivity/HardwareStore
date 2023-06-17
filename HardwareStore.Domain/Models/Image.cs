namespace HardwareStore.Domain.Models;

public class Image
{
    public byte[] Source { get; set; } = null!;
    public string MongoId { get; set; } = null!;
}