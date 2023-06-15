namespace HardwareStore.Data.Models;

public class CategoryDb
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long MainCategoryId { get; set; }

    public virtual ICollection<CategoryTitleDb> CategoryTitles { get; set; } = new List<CategoryTitleDb>();

    public virtual MainCategoryDb MainCategoryDb { get; set; } = null!;
}
