using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IPostRepository
    {
        Task<object> GetPost(int id);
    }
}