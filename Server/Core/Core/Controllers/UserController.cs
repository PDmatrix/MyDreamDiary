using System.Security.Claims;
using System.Threading.Tasks;
using Core.InputDTO;
using DB.Dto;
using DB.Interfaces;
using DB.OutputDto;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GetUserDtoOut>> GetUser([FromRoute] string id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return NotFound();
            
            return Ok(user);
        }
	    [Authorize]
        [HttpGet("dream/{id}")]
	    [ProducesResponseType(200)]
	    [ProducesResponseType(401)]
	    [ProducesResponseType(404)]
        public async Task<ActionResult<GetDreamDtoOut>> GetDream([FromRoute] int id)
        {
            var dream = await _userRepository.GetDreamAsync(id);

            if (dream == null)
                return NotFound();
            
            return Ok(dream);
        }
        
	    [Authorize]
        [HttpPost("{id}/dream")]
        [ProducesResponseType(201)]
	    [ProducesResponseType(401)]
	    [ProducesResponseType(404)]
        public async Task<ActionResult<AddDreamDtoOut>> AddDream([FromBody] AddDreamDtoIn dream)
        {
            (await new AddDreamDtoInValidator().ValidateAsync(dream)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _userRepository.AddDreamAsync(userId, dream.Content, dream.DreamDate);
            return CreatedAtAction(nameof(GetDream), res.Id, res);
        }
    }
}