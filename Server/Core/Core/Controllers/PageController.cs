using System.Threading.Tasks;
using Core.InputDTO;
using DB.Dto;
using DB.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Page<PageDto>>> GetPage([FromRoute] int index, [FromQuery] GetPageDto getPageDto)
        {
            (await new GetPageDtoValidator().ValidateAsync(getPageDto)).AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(await _pageRepository.GetPageAsync(index, getPageDto.PageSize, getPageDto.GetTags()));
        }
        
    }
}