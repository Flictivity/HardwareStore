using Npgsql;

namespace HardwareStore.Data.Read;

public class StoreReadonlyContext : IDisposable, IAsyncDisposable
{
    public NpgsqlConnection Connection { get; }

    public StoreReadonlyContext(string connectionString)
    {
        Connection = new NpgsqlConnection(connectionString);
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await Connection.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}