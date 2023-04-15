using ContentWriterService.Models;
using ContentWriterService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentWriterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentWriterController : Controller
    {
        private readonly IContentService _contentService;
        
        public ContentWriterController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpPost("/create")]
        public async Task<ActionResult<Content>> addContent([FromBody] Content content)
        {
           return await _contentService.addContent(content);
        }
    }
}
