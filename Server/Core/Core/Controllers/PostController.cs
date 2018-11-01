using System.Threading.Tasks;
using Core.Util;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            return Ok(await _postRepository.GetPost(id));
        }
        
        // TODO: Extract in separate file
        public class AddPostBody
        {
            public int UserId { get; set; }
            public int DreamId { get; set; }
            public string Title { get; set; }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] AddPostBody body)
        {
            // TODO: Add model validation
            var newPost = await _postRepository.AddPost(body.UserId, body.DreamId, body.Title);
            return CreatedAtAction(nameof(GetPost), 
                new { id = UtilHelper.GetValueFromAnonymousType<int>(newPost, "id") }, newPost);
        }
    }
}