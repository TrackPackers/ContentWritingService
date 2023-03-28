using ContentWriterService.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentWriterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentWriterController : Controller
    {
        private readonly KafkaController _kafkaController;
        
        public ContentWriterController(KafkaController kafkaController)
        {
            _kafkaController = kafkaController;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> addContent([FromBody] string message)
        {
            await _kafkaController.ProduceAsync("NEW_CONTENT", message);
            return Ok();
        }
    }
}
