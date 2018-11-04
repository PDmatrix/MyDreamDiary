using System;
using System.Threading.Tasks;
using DB.OutputDto;

namespace DB.Interfaces
{
    public interface IUserRepository
    {
        Task<GetUserDtoOut> GetUserAsync(string id);
        Task<GetDreamDtoOut> GetDreamAsync(int id);
        Task<AddDreamDtoOut> AddDreamAsync(string userId, string content, DateTime dreamDate);
	    Task<AddUserDtoOut> AddUserAsync(string id, string name, string email);
    }
}