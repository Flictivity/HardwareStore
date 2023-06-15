namespace HardwareStore.Data.Read.Queries;

public class CategoryRepositoryQueries
{
    public const string GetMainCategories = @"SELECT * FROM public.main_category";
    public const string UpdateCategory = @"UPDATE public.main_category SET name = @name WHERE id = @id";
}