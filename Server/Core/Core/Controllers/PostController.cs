using System.Security.Claims;
using System.Threading.Tasks;
using Core.InputDTO;
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
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var post = await _postRepository.GetPostAsync(id, userId);

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
            return CreatedAtAction(nameof(GetPost), new { id =  newPost.Id }, newPost);
        }
	    
	    [HttpGet("comment/{id}")]
	    [ProducesResponseType(201)]
	    [ProducesResponseType(404)]
	    public async Task<ActionResult<CommentDtoOut>> GetComment([FromRoute] int id)
	    {
		    var comment = await _postRepository.GetCommentAsync(id);
		    
		    if (comment == null)
			    return NotFound();
		    
		    return Ok(comment);
	    }
	    
	    [Authorize]
	    [HttpPost("{id}/like")]
	    [ProducesResponseType(200)]
	    [ProducesResponseType(401)]
	    public async Task<ActionResult> Like([FromRoute] int id)
	    {
		    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		    await _postRepository.LikeAsync(id, userId);
		    return Ok();
	    }
	    
	    [Authorize]
	    [HttpPost("{id}/comment")]
	    [ProducesResponseType(201)]
	    [ProducesResponseType(400)]
	    [ProducesResponseType(401)]
	    public async Task<ActionResult<CommentDtoOut>> AddComment([FromRoute] int id, [FromBody] AddCommentDtoIn comment)
	    {
		    (await new AddCommentDtoInValidator().ValidateAsync(comment)).AddToModelState(ModelState, null);
		    if (!ModelState.IsValid)
			    return BadRequest(ModelState);
	        
		    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		    var newComment = await _postRepository.AddCommentAsync(userId, id, comment.Content);
		    return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, newComment);
	    }
    }
}