using System.Threading.Tasks;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        
        [Route("getpost")]
        [HttpGet]
        public async Task<IActionResult> GetPost(int id)
        {
            return Json(await _postRepository.GetPost(id));
        }
    }
}