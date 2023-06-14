using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Data.Write;

public class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }
}