using System.Linq;
using System.Threading.Tasks;
using DB.Dto;
using DB.Entity;
using DB.Interfaces;
using DB.OutputDto;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<GetPostDtoOut> GetPostAsync(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Post
                    .AsQueryable()
                    .Select(r => new GetPostDtoOut
                    {
                        Title = r.Title,
                        Content = r.Dream.Content,
                        Username = r.User.Name,
                        Comments = r.Comment.Select(x => new CommentDtoOut
                        {
                            Id = x.Id,
                            Content = x.Content,
                            DateCreated = x.DateCreated
                        }),
	                    Tags = r.PostTag.Select(x => x.Tag.Name).ToArray(),
                        LikesCount = r.LikesCount,
                        DateCreated = r.DateCreated,
                        Id = r.Id
                    })
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
        }

        public async Task<AddPostDtoOut> AddPostAsync(string userId, int dreamId, string title)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var res = await context.Post.AddAsync(new Post {UserId = userId, DreamId = dreamId, Title = title});
                await context.SaveChangesAsync();
                return new AddPostDtoOut
                {
                    Id = res.Entity.Id,
                    Title = res.Entity.Title,
                    DateCreated = res.Entity.DateCreated
                };
            }
        }
    }
}