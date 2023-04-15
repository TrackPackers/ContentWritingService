using ContentWriterService.Context;
using ContentWriterService.Messaging;
using ContentWriterService.Models;
using ContentWriterService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

namespace ContentWriterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentWriterController : Controller
    {
        private readonly ContentService _contentService;
        
        public ContentWriterController(ContentService contentService)
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
