using System.Threading.Tasks;
using DB.Dto;
using DB.OutputDto;

namespace DB.Interfaces
{
    public interface IPostRepository
    {
        Task<GetPostDtoOut> GetPostAsync(int id);
        Task<AddPostDtoOut> AddPostAsync(string userId, int dreamId, string title);
    }
}