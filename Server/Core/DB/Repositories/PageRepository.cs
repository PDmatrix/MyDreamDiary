using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.OutputDto;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class PageRepository : BaseRepository, IPageRepository
    {
        public PageRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<Page<PageDtoOut>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags, string userId)
        {
            var result = new Page<PageDtoOut> { CurrentPage = index, PageSize = pageSize };

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Post
                    .AsQueryable()
                    .AsNoTracking();
                
                if (tags != null)
                {
                    var tagsEnumerable = tags as string[] ?? tags.ToArray();
                    query = query.Where(r => r.PostTag.Any(z => tagsEnumerable.Contains(z.Tag.Name)));
                }

                result.TotalPages = (int)Math.Ceiling((double) await query.CountAsync() / pageSize);

                var res = query
                    .Select(r => new PageDtoOut
                    {
                        Title = r.Title,
                        Content = r.Dream.Content,
                        Tags = r.PostTag.Select(x => x.Tag.Name).ToArray(),
                        Username = r.User.Name,
                        LikesCount = r.LikesCount,
                        DateCreated = r.DateCreated,
	                    Id = r.Id,
	                    IsLiked = userId != null &&
		                    r.UserLike.FirstOrDefault(x => x.PostId == r.Id && x.UserId == userId) != null,
	                    CommentsCount = r.Comment.Count
                    })
                    .OrderByDescending(r => r.DateCreated)
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize);
                
                result.Records = await res.ToListAsync();
            }
            return result;

        }
    }
}