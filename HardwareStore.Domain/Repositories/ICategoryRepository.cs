using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface ICategoryRepository
{
    Task CreateMainCategory(MainCategory mainCategory);
    Task CreateCategory(Category category);
    Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory);
    Task<BaseResult> UpdateCategory(Category newCategory);
    Task<IEnumerable<MainCategory>> GetMainCategories();
    Task<IEnumerable<Category>> GetCategories();
    Task<MainCategory?> GetMainCategory(long id);
}