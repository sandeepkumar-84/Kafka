using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092,localhost:9093" };

using var producer = new ProducerBuilder<string, string>(config).Build();

while (true)
{
    Console.WriteLine("Enter message to send:");
    var msg = Console.ReadLine();

    await producer.ProduceAsync("input-topic", new Message<string, string> { Key = null, Value = msg });
    Console.WriteLine($"Sent: {msg}");
}