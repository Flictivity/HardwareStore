using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface ICategoryService
{
    Task<BaseResult> UpdateMainCategoryAsync(MainCategory mainCategory, string name);
    Task<BaseResult> CreateMainCategoryAsync(string name);
    Task<IEnumerable<MainCategory>> GetMainCategoriesAsync();
}