using System.Threading.Tasks;
using DB.Models;

namespace DB.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetUser(int id);
    }
}