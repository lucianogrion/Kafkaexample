using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string topic = "topic-basic-test";

            CancellationTokenSource cts = new CancellationTokenSource();

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "test-consumer-group"
            };


            using (var consumer = new ConsumerBuilder<string, string>(config
                ).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    while (true) { 
                    var cr = consumer.Consume(cts.Token);

                    Console.WriteLine ( $"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
