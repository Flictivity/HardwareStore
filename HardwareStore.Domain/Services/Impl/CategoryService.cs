﻿using HardwareStore.Domain.Models;
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

    public async Task<BaseResult> UpdateCategoryAsync(Category category, string name, MainCategory mainCategory,
        List<CategoryTitle> titles)
    {
        var newCategory = new Category
        {
            Id = category.Id,
            Name = name,
            MainCategory = mainCategory,
        };
        return await _categoryRepository.UpdateCategory(newCategory, titles);
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

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetCategories();
    }

    public Task<Category?> GetCategory(long id)
    {
        return _categoryRepository.GetCategory(id);
    }

    public async Task<BaseResult> CreateCategoryAsync(string name, MainCategory mainCategory,
        List<CategoryTitle> titles)
    {
        var newCategory = new Category {Name = name, MainCategory = mainCategory};
        await _categoryRepository.CreateCategory(newCategory, titles);
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<CategoryTitle>> GetTitles(long categoryId)
    {
        return await _categoryRepository.GetTitles(categoryId);
    }

    public async Task<IEnumerable<NavMenuItem>> GetNavMenuItems()
    {
        return await _categoryRepository.GetNavMenuItems();
    }
}