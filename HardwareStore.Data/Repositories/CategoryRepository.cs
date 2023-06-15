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

    public async Task CreateCategory(Category category)
    {
        _context.Categories.Add(EntityConverter.ConvertCategory(category));
        await _context.SaveChangesAsync();
    }

    public async Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory)
    {
        await _readonlyContext.Connection.ExecuteAsync(CategoryRepositoryQueries.UpdateMainCategory,
            new {id = newMainCategory.Id, name = newMainCategory.Name});
        return new BaseResult {Success = true};
    }

    public async Task<BaseResult> UpdateCategory(Category newCategory)
    {
        await _readonlyContext.Connection.ExecuteAsync(CategoryRepositoryQueries.UpdateCategory,
            new {id = newCategory.Id, name = newCategory.Name, mainCategoryId = newCategory.MainCategory.Id});
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
}