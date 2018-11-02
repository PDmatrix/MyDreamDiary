using System.Threading.Tasks;
using DB.Entity;

namespace DB.Interfaces
{
    public interface IUserRepository
    {
        Task<object> GetUser(int id);
        Task<object> AddDream(Dream dream);
    }
}