using System.Threading.Tasks;
using DB.Entity;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<object>> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
                return NotFound();
            
            return Ok(user);
        }
        
        [HttpPost("{id}/dream")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<object>> AddDream([FromRoute] int id, [FromBody] Dream dream)
        {
            var newDream = await _userRepository.AddDream(dream);
            // TODO: Add nameof(GetDream) instead nameof(addDream)
            return CreatedAtAction(nameof(AddDream), newDream, newDream);
        }
    }
}