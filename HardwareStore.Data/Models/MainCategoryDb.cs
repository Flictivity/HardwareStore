namespace HardwareStore.Data.Models;

public class MainCategoryDb
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CategoryDb> Categories { get; set; } = new List<CategoryDb>();
}
