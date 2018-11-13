using System;
using System.Linq;
using System.Threading.Tasks;
using DB.Entity;
using DB.Interfaces;
using DB.OutputDto;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<AddDreamDtoOut> AddDreamAsync(string userId, string content, DateTime dreamDate)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var newDream = new Dream {Content = content, DreamDate = dreamDate, UserId = userId};
                var res = await context.Dream.AddAsync(newDream);
                await context.SaveChangesAsync();
                return new AddDreamDtoOut
                {
                    Id = res.Entity.Id,
                    Content = res.Entity.Content,
                    DreamDate = res.Entity.DreamDate
                };
            }
        }

	    public async Task<AddUserDtoOut> AddUserAsync(string id, string name, string email)
	    {
		    using (var context = ContextFactory.CreateDbContext(ConnectionString))
		    {
			    var user = await context.IdentityUser.FindAsync(id);
			    if (user != null)
				    return null;
			    
			    var newUser = new IdentityUser {Id = id, Email = email, Name = name};
			    var res = await context.IdentityUser.AddAsync(newUser);
			    await context.SaveChangesAsync();
			    return new AddUserDtoOut
			    {
				    Id = res.Entity.Id,
				    Name = res.Entity.Name,
				    Email = res.Entity.Email
			    };
		    }
	    }

	    public async Task<GetDreamDtoOut> GetDreamAsync(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Dream
                    .AsQueryable()
                    .AsNoTracking()
                    .Select(r => new GetDreamDtoOut
                    {
                        Id = r.Id,
                        Content = r.Content,
                        DreamDate= r.DreamDate
                    })
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
        }

        public async Task<GetUserDtoOut> GetUserAsync(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.IdentityUser
                    .AsQueryable()
                    .AsNoTracking()
                    .Select(r => new GetUserDtoOut
                    {
                        Name = r.Name,
                        Email = r.Email,
                        Comments = r.Comment.Select(x => new CommentDtoOut
                        {
                            Id = x.Id,
                            Content = x.Content,
                            DateCreated = x.DateCreated
                        }),
                        Posts = r.Post.Select(x => new UserPostDtoOut
                        {
                            Id = x.Id,
                            Title = x.Title
                        }),
                        Id = r.Id
                    })
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
        }
    }
}