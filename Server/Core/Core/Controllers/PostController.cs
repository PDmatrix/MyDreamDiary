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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        
        [HttpGet("{id}")]
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
        public async Task<ActionResult<AddPostDtoOut>> Create([FromBody] AddPostDtoIn body)
        {
            (await new AddPostDtoInValidator().ValidateAsync(body)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
	        
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newPost = await _postRepository.AddPostAsync(userId, body.DreamId, body.Title);
            return CreatedAtAction(nameof(GetPost), new { id =  newPost.Id }, newPost);
        }
	    
	    [HttpGet("comment/{id}")]
	    public async Task<ActionResult<CommentDtoOut>> GetComment([FromRoute] int id)
	    {
		    var comment = await _postRepository.GetCommentAsync(id);
		    
		    if (comment == null)
			    return NotFound();
		    
		    return Ok(comment);
	    }
	    
	    [Authorize]
	    [HttpPost("{id}/like")]
	    [ProducesResponseType(StatusCodes.Status200OK)]
	    public async Task<ActionResult> PostLike([FromRoute] int id)
	    {
		    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		    await _postRepository.LikeAsync(id, userId);
		    return Ok();
	    }
	    
	    [Authorize]
	    [HttpPost("{id}/comment")]
	    [ProducesResponseType(StatusCodes.Status201Created)]
	    [ProducesResponseType(StatusCodes.Status400BadRequest)]
	    public async Task<ActionResult<CommentDtoOut>> CreateComment(int id, AddCommentDtoIn comment)
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