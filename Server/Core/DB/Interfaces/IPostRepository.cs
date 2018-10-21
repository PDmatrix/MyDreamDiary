using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPost(int id);
    }
}