using Dapper;
using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Read.Queries;
using HardwareStore.Data.Write;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;

    public CategoryRepository(HardwareStoreContext context, HardwareStoreReadonlyContext readonlyContext)
    {
        _context = context;
        _readonlyContext = readonlyContext;
    }

    public async Task CreateMainCategory(MainCategory mainCategory)
    {
        _context.MainCategories.Add(EntityConverter.ConvertMainCategory(mainCategory));
        await _context.SaveChangesAsync();
    }

    public async Task CreateCategory(Category category, List<CategoryTitle> titles)
    {
        var createdCategory = _context.Categories.Add(EntityConverter.ConvertCategory(category));
        await _context.SaveChangesAsync();
        var categoryId = createdCategory.Entity.Id;
        foreach (var title in titles)
        {
            var newCategoryTitleDb = new CategoryTitleDb
            {
                Name = title.Name,
                CategoryId = categoryId
            };
            _context.CategoryTitles.Add(newCategoryTitleDb);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory)
    {
        await _readonlyContext.Connection.ExecuteAsync(CategoryRepositoryQueries.UpdateMainCategory,
            new {id = newMainCategory.Id, name = newMainCategory.Name});
        return new BaseResult {Success = true};
    }

    public async Task<BaseResult> UpdateCategory(Category newCategory, List<CategoryTitle> titles)
    {
        await _readonlyContext.Connection.ExecuteAsync(CategoryRepositoryQueries.UpdateCategory,
            new {id = newCategory.Id, name = newCategory.Name, mainCategoryId = newCategory.MainCategory.Id});
        foreach (var title in titles)
        {
            var newTitleDb = new CategoryTitleDb
            {
                Id = title.Id,
                Name = title.Name,
                CategoryId = newCategory.Id,
            };
            _context.CategoryTitles.Update(newTitleDb);
        }
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<MainCategory>> GetMainCategories()
    {
        var res = await _readonlyContext.Connection
            .QueryAsync<MainCategoryDb>(CategoryRepositoryQueries.GetMainCategories);

        return res.Select(EntityConverter.ConvertMainCategory);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var res = await _readonlyContext.Connection
            .QueryAsync<CategoryDb>(CategoryRepositoryQueries.GetCategories);

        List<Category> categories = new();
        foreach (var category in res)
        {
            var convertedCategory = EntityConverter.ConvertCategory(category);
            var mainCategory = await GetMainCategory(category.MainCategoryId);
            if (mainCategory is null)
                throw new Exception();
            convertedCategory.MainCategory = mainCategory;
            categories.Add(convertedCategory);
        }
        return categories;
    }

    public async Task<MainCategory?> GetMainCategory(long id)
    {
        var mainCategory = await _readonlyContext.Connection.QueryFirstOrDefaultAsync<MainCategoryDb>(
            CategoryRepositoryQueries.GetMainCategoryById, new {id});

        return mainCategory is null ? null : EntityConverter.ConvertMainCategory(mainCategory);
    }
    
    public async Task<Category?> GetCategory(long id)
    {
        var categoryDb = await _readonlyContext.Connection.QueryFirstOrDefaultAsync<CategoryDb>(
            CategoryRepositoryQueries.GetCategoryById, new {id});

        if (categoryDb is null)
            return null;
        var category = EntityConverter.ConvertCategory(categoryDb);
        var mainCategory = await GetMainCategory(categoryDb.MainCategoryId);
        if (mainCategory is null)
            return null;
        category.MainCategory = mainCategory;
        return category;
    }

    public async Task<IEnumerable<CategoryTitle>> GetTitles(long categoryId)
    {
        var categoryTitles = new List<CategoryTitle>();
        var res = await _readonlyContext.Connection
            .QueryAsync<CategoryTitleDb>(CategoryRepositoryQueries.GetCategoryTitles, new {id = categoryId});

        foreach (var title in res)
        {
            var converted = EntityConverter.ConvertCategoryTitle(title);
            var category = await GetCategory(title.CategoryId);
            if (category is null)
                throw new Exception();
            converted.Category = category;
            categoryTitles.Add(converted);
        }
        return categoryTitles;
    }
}