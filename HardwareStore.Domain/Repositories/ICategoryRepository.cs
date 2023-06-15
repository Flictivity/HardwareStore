using HardwareStore.Domain.Models;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Repositories;

public interface ICategoryRepository
{
    Task CreateMainCategory(MainCategory mainCategory);
    Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory);
    Task<IEnumerable<MainCategory>> GetMainCategories();
}