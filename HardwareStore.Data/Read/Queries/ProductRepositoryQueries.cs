namespace HardwareStore.Data.Read.Queries;

public class ProductRepositoryQueries
{
    public const string GetProductById = @"SELECT * FROM public.product p WHERE id = @id";

    public const string GetExistProductCharacteristics =
        @"SELECT ct.name, ctv.value, ctv.id, ct.id AS CategoryTitleId FROM public.category_title_value ctv 
            JOIN category_title ct on ctv.category_title_id = ct.id WHERE ctv.product_id = @id";

    public const string GetProductCategory = @"SELECT c.* FROM category c 
                JOIN category_title ct on c.id = ct.category_id 
                JOIN category_title_value ctv on ctv.category_title_id = ct.id 
                WHERE ctv.product_id = @id LIMIT 1";

    public const string GetProductImages =
        @"SELECT p.image_source FROM public.product_image p WHERE p.product_id = @id";

    public const string GetProductCharacteristics = @"SELECT ct.name, '', 0, ct.id AS CategoryTitleId FROM public.category_title ct WHERE ct.category_id = @id";
}