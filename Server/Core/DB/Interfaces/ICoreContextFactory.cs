using DB.Context;

namespace DB.Interfaces
{
    public interface ICoreContextFactory
    {
        CoreContext CreateDbContext(string connectionString);
    }
}