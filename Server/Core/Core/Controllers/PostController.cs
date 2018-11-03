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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GetPostDtoOut>> GetPost([FromRoute] int id)
        {
            var post = await _postRepository.GetPostAsync(id);

            if (post == null)
                return NotFound();
            
            return Ok(post);
        }
        
	    [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AddPostDtoOut>> AddPost([FromBody] AddPostDtoIn body)
        {
            (await new AddPostDtoInValidator().ValidateAsync(body)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
	        
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newPost = await _postRepository.AddPostAsync(userId, body.DreamId, body.Title);
            return CreatedAtAction(nameof(GetPost), newPost.Id, newPost);
        }
    }
}