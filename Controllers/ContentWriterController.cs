using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentWriterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentWriterController : Controller
    {
        [HttpGet("/create")]
        public String createData()
        {
            return "works";
        }
    }
}
