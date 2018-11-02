using System.Linq;
using System.Threading.Tasks;
using DB.Entity;
using DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<object> GetPost(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Post
                    .AsQueryable()
                    .Select(r => new
                    {
                        title = r.Title,
                        content = r.Dream.Content,
                        user = r.User.Name,
                        comments = r.Comment,
                        likes_count = r.LikesCount,
                        date_created = r.DateCreated,
                        id = r.Id
                    })
                    .SingleOrDefaultAsync(r => r.id == id);
            }
        }

        public async Task<object> AddPost(int userId, int dreamId, string title)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var res = await context.Post.AddAsync(new Post {UserId = userId, DreamId = dreamId, Title = title});
                await context.SaveChangesAsync();
                return new
                {
                    id = res.Entity.Id,
                    title = res.Entity.Title,
                    date = res.Entity.DateCreated
                };
            }
        }
    }
}