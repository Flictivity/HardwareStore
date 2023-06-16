using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface ICategoryService
{
    Task<BaseResult> UpdateMainCategoryAsync(MainCategory mainCategory, string name);

    Task<BaseResult> UpdateCategoryAsync(Category category, string name, MainCategory mainCategory,
        List<CategoryTitle> titles);

    Task<BaseResult> CreateMainCategoryAsync(string name);
    Task<IEnumerable<MainCategory>> GetMainCategoriesAsync();
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategory(long id);

    Task<BaseResult> CreateCategoryAsync(string name, MainCategory mainCategory, List<CategoryTitle> titles);

    Task<IEnumerable<CategoryTitle>> GetTitles(long categoryId);
}