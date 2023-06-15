using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResult> UpdateMainCategoryAsync(MainCategory mainCategory, string name)
    {
        var newMainCategory = new MainCategory
        {
            Id = mainCategory.Id,
            Name = name
        };
        return await _categoryRepository.UpdateMainCategory(newMainCategory);
    }

    public async Task<BaseResult> CreateMainCategoryAsync(string name)
    {
        var newMainCategory = new MainCategory {Name = name};
        await _categoryRepository.CreateMainCategory(newMainCategory);
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<MainCategory>> GetMainCategoriesAsync()
    {
        return await _categoryRepository.GetMainCategories();
    }
}