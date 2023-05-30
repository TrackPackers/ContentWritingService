using Confluent.Kafka;
using System.Text;
using System.Text.Unicode;
using Kafka.Public;
using Kafka.Public.Loggers;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using ContentWriterService.Services;
using ContentWriterService.Services.Interfaces;

namespace ContentWriterService.Messaging
{
    public class KafkaConsumer : IHostedService
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private IClusterClient _cluster;
        private IContentService _contentService;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IContentService contentService, IConfiguration configuration)
        {
            _logger = logger;
            _contentService = contentService;

            _cluster = new ClusterClient(new Configuration
            {
                Seeds = configuration["KAFKA_URI"],
            }, new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("DELETE_USER");

            _cluster.MessageReceived += async record =>
            {
                _logger.LogInformation(Encoding.UTF8.GetString(record.Value as byte[]));

                var json = JsonObject.Parse(Encoding.UTF8.GetString(record.Value as byte[]));

                if(record.Topic == "DELETE_USER")
                {
                   await _contentService.deleteUser((string)json["Name"]);
                }

            };
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster.Dispose();
            return Task.CompletedTask;
        }
    }
}
