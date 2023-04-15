using ContentWriterService.Context.Interfaces;
using ContentWriterService.Messaging.Interfaces;
using ContentWriterService.Models;
using ContentWriterService.Services;
using ContentWriterService.Services.Interfaces;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace ContentWriterService.Tests
{
    public class ContentServiceTest
    {
        private readonly Mock<IKafkaController> _kafkaController;
        private readonly Mock<IDbContentContext> _dbContentContext;
        private readonly IContentService _contentService;

        public ContentServiceTest()
        {
            _kafkaController = new Mock<IKafkaController>();
            _dbContentContext = new Mock<IDbContentContext>();
            _contentService = new ContentService(_kafkaController.Object, _dbContentContext.Object);
            _dbContentContext.Setup(x => x.Contents).Returns(new Mock<IMongoCollection<Content>>().Object);
        }

        [Fact]
        public async Task addContent()
        {
            // Arrange
            var content = new Content
            {
                Id = new MongoDB.Bson.ObjectId(),
                Name = "Test_NAME",
                Message = "Test_MESSAGE"
            };

            // Act
            var result = await _contentService.addContent(content);

            // Assert
            Assert.Equal(content, result);
        }
    }
}