using Confluent.Kafka;
using ContentWriterService.Messaging.Interfaces;

namespace ContentWriterService.Messaging
{
    public class KafkaController : IKafkaController
    {
        private readonly ProducerConfig _config;

        public KafkaController(IConfiguration configuration)
        {
            Console.WriteLine(configuration["KAFKA_URI"]);
            _config = new ProducerConfig { BootstrapServers = configuration["KAFKA_URI"] };
        }

        public async Task ProduceAsync(string topic, string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}