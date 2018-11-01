using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IPostRepository
    {
        Task<object> GetPost(int id);
        Task<object> AddPost(int userId, int dreamId, string title);
    }
}