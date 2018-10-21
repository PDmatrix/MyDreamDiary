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

        public async Task<Page<Post>> GetPageAsync(int index)
        {
            return await GetPageAsync(index, 10);
        }

        public async Task<Page<Post>> GetPageAsync(int index, int pageSize)
        {
            return await GetPageAsync(index, pageSize, null);
        }

        public async Task<Page<Post>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags)
        {
            var result = new Page<Post> { CurrentPage = index, PageSize = pageSize };

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Post.AsQueryable(); 
                if (tags != null)
                {
                    var tagsEnumerable = tags as string[] ?? tags.ToArray();
                    query = query.Where(r => r.Tags.Any(z => tagsEnumerable.Contains(z.Name)));
                }

                result.TotalPages = (int)Math.Ceiling((double) await query.CountAsync() / pageSize);
                
                query = query
                    .Include(r => r.Dream)
                    .Include(r => r.Comments)
                    .Include(r => r.Tags)
                    .Include(r => r.User)
                    .OrderByDescending(p => p.DateCreated)
                    .Skip(index * pageSize)
                    .Take(pageSize); 
                result.Records = await query.ToListAsync();
            }
            return result;

        }
    }
}