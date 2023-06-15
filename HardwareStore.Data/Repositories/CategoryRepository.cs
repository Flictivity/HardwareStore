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

    public async Task<BaseResult> UpdateMainCategory(MainCategory newMainCategory)
    {
        await _readonlyContext.Connection.ExecuteAsync(CategoryRepositoryQueries.UpdateCategory,
            new {id = newMainCategory.Id, name = newMainCategory.Name});
        return new BaseResult {Success = true};
    }

    public async Task<IEnumerable<MainCategory>> GetMainCategories()
    {
        var res = await _readonlyContext.Connection
            .QueryAsync<MainCategoryDb>(CategoryRepositoryQueries.GetMainCategories);

        return res.Select(EntityConverter.ConvertMainCategory);
    }
}