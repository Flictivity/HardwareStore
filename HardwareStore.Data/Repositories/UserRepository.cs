using Dapper;
using HardwareStore.Data.Models;
using HardwareStore.Data.Read;
using HardwareStore.Data.Read.Queries;
using HardwareStore.Data.Write;
using HardwareStore.Domain.ErrorMessages;
using HardwareStore.Domain.Models;
using HardwareStore.Domain.Repositories;
using HardwareStore.Domain.Results;

namespace HardwareStore.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HardwareStoreContext _context;
    private readonly HardwareStoreReadonlyContext _readonlyContext;

    public UserRepository(HardwareStoreContext context, HardwareStoreReadonlyContext readonlyContext)
    {
        _context = context;
        _readonlyContext = readonlyContext;
    }

    public async Task<User?> GetUserAsync(string email)
    {
        var user = await _readonlyContext.Connection.QueryFirstOrDefaultAsync<UserDb>(
            UserRepositoryQueries.GetUserByEmail, new {email});

        return user is null ? null : EntityConverter.ConvertUser(user);
    }

    public async Task<User?> GetUserAsync(long id)
    {
        var user = await _readonlyContext.Connection.QueryFirstOrDefaultAsync<UserDb>(
            UserRepositoryQueries.GetUserById, new {id});

        return user is null ? null : EntityConverter.ConvertUser(user);
    }

    public async Task<string> GetUserEncryptedPassword(long userId)
    {
        return await _readonlyContext.Connection.QuerySingleOrDefaultAsync<string>(
            UserRepositoryQueries.GetUserEncryptedPassword, new {id = userId});
    }

    public async Task CreateUserAsync(User user, string encryptedPassword)
    {
        var dbUser = EntityConverter.ConvertUser(user);
        dbUser.Password = encryptedPassword;
        _context.Users.Add(dbUser);
        await _context.SaveChangesAsync();
    }

    public async Task<BaseResult> ChangeUserData(User user, string encryptedPassword)
    {
        var dbUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
        if(dbUser is null)
            return new BaseResult{Success = false, Message = UserErrorMessages.NotFound};
        dbUser.Email = user.Email;
        dbUser.Password = encryptedPassword;
        dbUser.FirstName = user.FirstName;
        dbUser.LastName = user.LastName;

        await _context.SaveChangesAsync();

        return new BaseResult {Success = true};
    }
}