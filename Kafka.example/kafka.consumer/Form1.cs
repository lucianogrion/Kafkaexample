using Confluent.Kafka;
using System.Configuration;

namespace kafka.consumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
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
                    
                        var cr = consumer.Consume(cts.Token);
                        
                        textBox1.Text = $"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}";
                      
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