using System.Threading.Tasks;
using DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        
        [Route("page")]
        [HttpGet]
        public async Task<IActionResult> GetPage(int index)
        {
            return Json(await _pageRepository.GetPageAsync(index));
        }
        
    }
}