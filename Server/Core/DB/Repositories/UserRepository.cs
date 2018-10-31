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


        public async Task<object> GetUser(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.IdentityUser
                    .AsQueryable()
                    .Select(r => new
                    {
                        name = r.Name,
                        email = r.Email,
                        comments = r.Comment,
                        posts = r.Post.Select(x => new
                        {
                            id = x.Id,
                            title = x.Title
                        }),
                        id = r.Id
                    })
                    .FirstOrDefaultAsync(r => r.id == id);
            }
        }
    }
}