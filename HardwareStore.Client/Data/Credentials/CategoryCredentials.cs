using HardwareStore.Domain.Models;

namespace HardwareStore.Client.Data.Credentials;

public class CategoryCredentials
{
    public string Name { get; set; } = null!;
    public MainCategory MainCategory { get; set; } = null!;
    public List<CategoryTitle> Titles { get; set; } = new()!;
}