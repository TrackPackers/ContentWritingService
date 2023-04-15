namespace ContentWriterService.Messaging.Interfaces
{
    public interface IKafkaController
    {
        public Task ProduceAsync(string topic, string message);
    }
}
