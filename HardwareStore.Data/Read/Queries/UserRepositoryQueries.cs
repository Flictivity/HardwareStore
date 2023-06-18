namespace HardwareStore.Data.Read.Queries;

public class UserRepositoryQueries
{
    public const string GetUserByEmail = @"SELECT * FROM public.user WHERE email = @email";
    public const string GetUserById = @"SELECT * FROM public.user WHERE id = @id";
    public const string GetUserEncryptedPassword = @"SELECT password FROM public.user WHERE id = @id";
}