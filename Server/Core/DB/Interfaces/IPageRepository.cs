using System.Collections.Generic;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IPageRepository
    {
        Task<Page<Post>> GetPageAsync(int index);
        Task<Page<Post>> GetPageAsync(int index, int pageSize);
        Task<Page<Post>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags);
    }
}