namespace HardwareStore.Domain.Models;

public class NavMenuItem
{
    public string MainCategoryName { get; set; } = null!;
    public long MainCategoryId { get; set; }
    public List<Category> Categories { get; set; } = null!;
}