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

        public async Task<Post> GetPost(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Post
                    .AsQueryable()
                    .Include(r => r.PostTag)
                        .ThenInclude(t => t.Tag)
                    .Include(r => r.Dream)
                    .Include(r => r.User);
                
                return await query.FirstAsync(r => r.Id == id);
            }
        }
    }
}