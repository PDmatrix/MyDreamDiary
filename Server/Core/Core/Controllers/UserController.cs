using System.Threading.Tasks;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [Route("user")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            return Json(await _userRepository.GetUser(id));
        }
    }
}