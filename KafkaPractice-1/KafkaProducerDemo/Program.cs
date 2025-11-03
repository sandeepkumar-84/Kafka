using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092,localhost:9093"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

Console.WriteLine("Enter messages (type 'exit' to quit):");


while (true)
{
    var message = Console.ReadLine();
    if (message == "exit") break;

    var result = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value = message });
    Console.WriteLine($"Delivered '{result.Value}' to: {result.TopicPartitionOffset}");
}