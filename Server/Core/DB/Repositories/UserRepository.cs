using System.Linq;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }


        public async Task<IdentityUser> GetUser(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.IdentityUser
                    .AsQueryable()
                    .Include(r => r.Comments)
                    .Include(r => r.Posts);
                
                return await query.FirstAsync(r => r.Id == id);
            }
        }
    }
}