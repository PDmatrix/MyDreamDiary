using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            return Ok(await _userRepository.GetUser(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddDream([FromBody] Dream dream)
        {
            // TODO: Add nameof(GetDream) instead nameof(addDream)
            return CreatedAtAction(nameof(AddDream), await _userRepository.AddDream(dream));
        }
    }
}