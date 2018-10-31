using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class PageRepository : BaseRepository, IPageRepository
    {
        public PageRepository(string connectionString, ICoreContextFactory contextFactory) 
            : base(connectionString, contextFactory)
        {
        }

        public async Task<Page<object>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags)
        {
            var result = new Page<object> { CurrentPage = index, PageSize = pageSize };

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Post
                    .AsQueryable();
                
                if (tags != null)
                {
                    var tagsEnumerable = tags as string[] ?? tags.ToArray();
                    query = query.Where(r => r.PostTag.Any(z => tagsEnumerable.Contains(z.Tag.Name)));
                }

                result.TotalPages = (int)Math.Ceiling((double) await query.CountAsync() / pageSize);

                var res = query.Select(r => new
                    {
                        title = r.Title,
                        content = r.Dream.Content,
                        tags = r.PostTag.Select(x => x.Tag.Name),
                        user = r.User.Name,
                        likes_count = r.LikesCount,
                        date_created = r.DateCreated
                    })
                    .OrderByDescending(r => r.date_created)
                    .Skip(index * pageSize)
                    .Take(pageSize);
                
                result.Records = await res.ToListAsync();
            }
            return result;

        }
    }
}