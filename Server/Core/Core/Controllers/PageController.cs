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
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        
        [HttpGet("{index=1}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Page<PageDtoOut>>> 
	        GetPage([FromRoute] int index, [FromQuery] GetPageDtoIn getPageDtoIn)
        {
            (await new GetPageDtoInValidator().ValidateAsync(getPageDtoIn)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
	        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _pageRepository.GetPageAsync(index, getPageDtoIn.PageSize, getPageDtoIn.GetTags(), userId));
        }
        
    }
}