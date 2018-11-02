using System.Linq;
using System.Threading.Tasks;
using DB.Entity;
using DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<object> AddDream(Dream dream)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var res = await context.Dream.AddAsync(dream);
                await context.SaveChangesAsync();
                return new
                {
                    id = res.Entity.Id,
                    content = res.Entity.Content,
                    date = res.Entity.DreamDate
                };
            }
        }
        
        public async Task<object> GetUser(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.IdentityUser
                    .AsQueryable()
                    .AsNoTracking()
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
                    .SingleOrDefaultAsync(r => r.id == id);
            }
        }
    }
}