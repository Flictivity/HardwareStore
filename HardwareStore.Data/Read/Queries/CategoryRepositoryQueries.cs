namespace HardwareStore.Data.Read.Queries;

public class CategoryRepositoryQueries
{
    public const string GetMainCategories = @"SELECT * FROM public.main_category";
    public const string GetCategories = @"SELECT * FROM public.category";
    public const string UpdateMainCategory = @"UPDATE public.main_category SET name = @name WHERE id = @id";

    public const string UpdateCategory =
        @"UPDATE public.category SET name = @name, main_category_id = @mainCategoryId WHERE id = @id";

    public const string GetMainCategoryById = @"SELECT * FROM public.main_category m WHERE id = @id";
    public const string GetCategoryById = @"SELECT * FROM public.category m WHERE id = @id";

    public const string GetCategoryTitles = @"SELECT * FROM public.category_title WHERE category_id = @id";
}