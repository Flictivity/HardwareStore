using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface ICategoryRepository
{
    Task CreateMainCategory(MainCategory mainCategory);
    Task CreateCategory(Category category, List<CategoryTitle> titles);
    Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory);
    Task<BaseResult> UpdateCategory(Category newCategory, List<CategoryTitle> titles);
    Task<IEnumerable<MainCategory>> GetMainCategories();
    Task<IEnumerable<Category>> GetCategories();
    Task<MainCategory?> GetMainCategory(long id);
    Task<Category?> GetCategory(long id);
    Task<IEnumerable<CategoryTitle>> GetTitles(long categoryId);
    Task<IEnumerable<NavMenuItem>> GetNavMenuItems();
}