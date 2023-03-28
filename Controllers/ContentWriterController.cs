using ContentWriterService.Context;
using ContentWriterService.Messaging;
using ContentWriterService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

namespace ContentWriterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentWriterController : Controller
    {
        private readonly KafkaController _kafkaController;
        private readonly DbContentContext _dbContentContext;
        
        public ContentWriterController(KafkaController kafkaController, DbContentContext dbContentContext)
        {
            _kafkaController = kafkaController;
            _dbContentContext = dbContentContext;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> addContent([FromBody] Content content)
        {
            await _dbContentContext.Contents.InsertOneAsync(content);
            await _kafkaController.ProduceAsync("NEW_CONTENT", Newtonsoft.Json.JsonConvert.SerializeObject(content));
            return Ok();
        }
    }
}
