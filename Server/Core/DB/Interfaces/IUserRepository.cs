using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IUserRepository
    {
        Task<object> GetUser(int id);
        Task<object> AddDream(Dream dream);
    }
}