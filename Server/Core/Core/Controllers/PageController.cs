using System.Threading.Tasks;
using Core.InputDTO;
using DB.Dto;
using DB.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        
        [HttpGet("{index=1}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Page<PageDtoOut>>> GetPage([FromRoute] int index, [FromQuery] GetPageDtoIn getPageDtoIn)
        {
            (await new GetPageDtoInValidator().ValidateAsync(getPageDtoIn)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(await _pageRepository.GetPageAsync(index, getPageDtoIn.PageSize, getPageDtoIn.GetTags()));
        }
        
    }
}