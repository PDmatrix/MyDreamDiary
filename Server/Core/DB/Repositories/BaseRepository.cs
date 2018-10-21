using DB.Interfaces;

namespace DB.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }
        protected ICoreContextFactory ContextFactory { get; }

        protected BaseRepository(string connectionString, ICoreContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = contextFactory;
        }
    }
}