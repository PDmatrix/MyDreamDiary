using System.Collections.Generic;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IPageRepository
    {
        Task<Page<object>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags);
    }
}