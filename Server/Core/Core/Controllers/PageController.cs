using System.Threading.Tasks;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        
        [HttpGet("{index=1}")]
        public async Task<IActionResult> GetPage([FromRoute] int index,[FromQuery] int pageSize = 10,[FromQuery] string tags = null)
        {
            return Ok(await _pageRepository.GetPageAsync(index, pageSize, tags?.Split(",")));
        }
        
    }
}