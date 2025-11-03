using Confluent.Kafka;
using System;

class ConsumerExample
{
    static void Main()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092,localhost:9093",
            GroupId = "stream-output-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe("output-topic");

        while (true)
        {
            var cr = consumer.Consume();
            Console.WriteLine($"Word: {cr.Message.Key}, UpperCase Word: {cr.Message.Value}");
        }
    }
}
