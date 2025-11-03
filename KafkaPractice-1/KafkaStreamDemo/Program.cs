using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using System;


        var config = new StreamConfig<StringSerDes, StringSerDes>
        {
            ApplicationId = "dotnet-kafka-streams-demo",
            BootstrapServers = "localhost:9092,localhost:9093"
        };

        var builder = new StreamBuilder();

        // 1️⃣ Read from input-topic
        var stream = builder.Stream<string, string>("input-topic");

        // 2️⃣ Split messages into words
        //var wordsStream = stream.FlatMapValues<string>(value =>
        //    value.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        //);

        var wordsStreamUpper = stream.MapValues(value =>
            {                
                return value.ToUpper();
            });
// 3️⃣ Group by word
//var grouped = wordsStream.GroupBy((key, word) => word);

//        // 4️⃣ Count occurrences
//        var counted = grouped.Count();


// 5️⃣ Convert KTable to stream and write to output-topic
        wordsStreamUpper.To("output-topic");

        // Console.WriteLine($"LogTest Point1 Word {grouped} Counted:{counted}");

// Build topology
var topology = builder.Build();

        var kafkaStream = new KafkaStream(topology, config);

        // Handle Ctrl+C for graceful exit
        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("Exiting Kafka Streams...");
            // There is no Close/StopAsync in your version; just exit
            Environment.Exit(0);
        };

        Console.WriteLine("Starting Kafka Streams. Press Ctrl+C to stop...");

        // Start stream (blocking)
        kafkaStream.Start();
