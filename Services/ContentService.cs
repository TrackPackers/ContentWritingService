using ContentWriterService.Context;
using ContentWriterService.Messaging;
using ContentWriterService.Models;
using ContentWriterService.Services.Interfaces;

namespace ContentWriterService.Services
{
    public class ContentService : IContentService
    {

        private readonly KafkaController _kafkaController;
        private readonly DbContentContext _dbContentContext;

        public ContentService(KafkaController kafkaController, DbContentContext dbContentContext) 
        {
            _kafkaController = kafkaController;
            _dbContentContext = dbContentContext;
        }


        public async Task<Content> addContent(Content content)
        {
            try
            {
                await _dbContentContext.Contents.InsertOneAsync(content);
                await _kafkaController.ProduceAsync("NEW_CONTENT", Newtonsoft.Json.JsonConvert.SerializeObject(content));
                return content;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
