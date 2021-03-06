using System.Collections.Generic;
using System.Threading.Tasks;
using DB.OutputDto;

namespace DB.Interfaces
{
    public interface IPageRepository
    {
        Task<Page<PageDtoOut>> GetPageAsync(int index, int pageSize, IEnumerable<string> tags, string userId);
    }
}