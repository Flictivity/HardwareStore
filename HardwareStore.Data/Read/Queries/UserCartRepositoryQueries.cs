namespace HardwareStore.Data.Read.Queries;

public class UserCartRepositoryQueries
{
    public const string GetProductsForCart = @"SELECT uc.product_id FROM public.user_cart uc WHERE uc.user_id = @id";
    public const string GetProductsForFavorite = @"SELECT uf.product_id FROM public.user_favorite uf WHERE uf.user_id = @id";
}