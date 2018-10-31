using System.Linq;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
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
                    .FirstOrDefaultAsync(r => r.id == id);
            }
        }
    }
}