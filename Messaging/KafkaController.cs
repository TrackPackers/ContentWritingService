using Confluent.Kafka;

namespace ContentWriterService.Messaging
{
    public class KafkaController
    {
        private readonly ProducerConfig _config;

        public KafkaController()
        {
            _config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        }

        public async Task ProduceAsync(string topic, string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
