using DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DB.Context
{
    public class CoreContextFactory : ICoreContextFactory
    {
        public CoreContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoreContext>();

            optionsBuilder.UseNpgsql(connectionString);
            return new CoreContext(optionsBuilder.Options);
        }
    }
}