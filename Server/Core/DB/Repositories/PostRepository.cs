using System.Linq;
using System.Threading.Tasks;
using DB.Entity;
using DB.Interfaces;
using DB.OutputDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DB.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<GetPostDtoOut> GetPostAsync(int id, string userId)
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
                            DateCreated = x.DateCreated,
	                        Username = x.User.Name
                        }),
	                    Tags = r.PostTag.Select(x => x.Tag.Name).ToArray(),
                        LikesCount = r.LikesCount,
                        DateCreated = r.DateCreated,
                        Id = r.Id,
	                    IsLiked = userId != null &&
	                              r.UserLike.FirstOrDefault(x => x.PostId == r.Id && x.UserId == userId) != null
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

	    public async Task<CommentDtoOut> AddCommentAsync(string userId, int postId, string content)
	    {
		    using (var context = ContextFactory.CreateDbContext(ConnectionString))
		    {
			    var res = await context.Comment.AddAsync(new Comment {Content = content, PostId = postId, UserId = userId});
			    await context.SaveChangesAsync();
			    return new CommentDtoOut
			    {
				    Id = res.Entity.Id,
				    Content = res.Entity.Content,
				    DateCreated = res.Entity.DateCreated
			    };
		    }
	    }

	    public async Task<CommentDtoOut> GetCommentAsync(int id)
	    {
		    using (var context = ContextFactory.CreateDbContext(ConnectionString))
		    {
			    return await context.Comment
				    .AsQueryable()
				    .Select(r => new CommentDtoOut
				    {
					    Content = r.Content,
					    Id = r.Id,
					    DateCreated = r.DateCreated
				    })
				    .SingleOrDefaultAsync(r => r.Id == id);
		    }
	    }

	    public async Task LikeAsync(int id, string userId)
	    {
		    using (var context = ContextFactory.CreateDbContext(ConnectionString))
		    {
			    var like = await context.UserLike
				                  .SingleOrDefaultAsync(r => r.PostId == id && r.UserId == userId);

			    if (like == null)
				    await context.UserLike.AddAsync(new UserLike {PostId = id, UserId = userId});
			    else
				    context.UserLike.Remove(like);
		    }
	    }
    }
}