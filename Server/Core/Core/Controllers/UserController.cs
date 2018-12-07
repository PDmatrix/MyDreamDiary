using System.Security.Claims;
using System.Threading.Tasks;
using Core.InputDTO;
using DB.Interfaces;
using DB.OutputDto;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDtoOut>> GetUser([FromRoute] string id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return NotFound();
            
            return Ok(user);
        }
	    [Authorize]
        [HttpGet("dream/{id}")]
        public async Task<ActionResult<GetDreamDtoOut>> GetDream([FromRoute] int id)
        {
            var dream = await _userRepository.GetDreamAsync(id);

            if (dream == null)
                return NotFound();
            
            return Ok(dream);
        }
	    
	    [Authorize]
	    [HttpPost]
	    [ProducesResponseType(StatusCodes.Status400BadRequest)]
	    [ProducesResponseType(StatusCodes.Status201Created)]
	    [ProducesResponseType(StatusCodes.Status200OK)]
	    public async Task<ActionResult<AddUserDtoOut>> CreateUser()
	    {
		    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		    var userName = User.FindFirst("nickname")?.Value;
		    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

		    if (userId == null || userName == null || userEmail == null)
			    return BadRequest();
		    
		    var res = await _userRepository.AddUserAsync(userId, userName, userEmail);
		    if (res == null)
			    return Ok();
		    
		    return CreatedAtAction(nameof(GetUser), new { id = res.Id }, res);
	    }
	    
	    [Authorize]
	    [HttpDelete("dream/{id}")]
	    public async Task<ActionResult<GetDreamDtoOut>> DeleteDream([FromRoute] int id)
	    {
		    var res = await _userRepository.DeleteDreamAsync(id);
		    return Ok(res);
	    }
        
	    [Authorize]
        [HttpPost("dream")]
        public async Task<ActionResult<GetDreamDtoOut>> CreateDream([FromBody] AddDreamDtoIn dream)
        {
            (await new AddDreamDtoInValidator().ValidateAsync(dream)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _userRepository.AddDreamAsync(userId, dream.Content, dream.DreamDate);
            return CreatedAtAction(nameof(GetDream), new { id = res.Id }, res);
        }
    }
}