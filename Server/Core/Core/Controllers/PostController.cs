using System.Threading.Tasks;
using Core.InputDTO;
using Core.Util;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<object>> GetPost([FromRoute] int id)
        {
            var post = await _postRepository.GetPost(id);

            if (post == null)
                return NotFound();
            
            return Ok(post);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<object>> AddPost([FromBody] AddPostDto body)
        {
            //(await new GetPageDtoValidator().ValidateAsync(pageGetDto)).AddToModelState(ModelState, null);
            //if (!ModelState.IsValid)
                //return BadRequest(ModelState);
            
            // TODO: Add model validation
            var newPost = await _postRepository.AddPost(body.UserId, body.DreamId, body.Title);
            return CreatedAtAction(nameof(GetPost), 
                new { id = UtilHelper.GetValueFromAnonymousType<int>(newPost, "id") }, newPost);
        }
    }
}