using ContentWriterService.Messaging.Interfaces;
using ContentWriterService.Models;
using ContentWriterService.Services.Interfaces;
using ContentWriterService.Context.Interfaces;

namespace ContentWriterService.Services
{
    public class ContentService : IContentService
    {

        private readonly IKafkaController _kafkaController;
        private readonly IDbContentContext _dbContentContext;

        public ContentService(IKafkaController kafkaController, IDbContentContext dbContentContext) 
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
