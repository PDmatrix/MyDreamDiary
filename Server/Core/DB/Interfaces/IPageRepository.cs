using System.Collections.Generic;
using System.Threading.Tasks;
using DB.Dto;

namespace DB.Interfaces
{
    public interface IPageRepository
    {
        Task<Page<PageDto>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags);
    }
}