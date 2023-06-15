namespace HardwareStore.Data.Models;

public class CategoryTitleDb
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long CategoryId { get; set; }

    public virtual CategoryDb CategoryDb { get; set; } = null!;

    public virtual ICollection<CategoryTitleValueDb> CategoryTitleValues { get; set; } = new List<CategoryTitleValueDb>();
}
