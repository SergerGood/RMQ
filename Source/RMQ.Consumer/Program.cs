using System;
using System.Text;

using RMQ.Consumer.Properties;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace RMQ.Consumer
{
    internal class Program
    {
        private static QueueingBasicConsumer CreateConsumer(IModel channel)
        {
            var consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(Settings.Default.QueueName, true, consumer);

            return consumer;
        }


        private static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = Settings.Default.HostName
            };

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(Settings.Default.QueueName, false, false, false, null);

                    QueueingBasicConsumer consumer = CreateConsumer(channel);

                    Console.WriteLine("-> Waiting for messages. To exit press CTRL+C");
                    while (true)
                    {
                        ReceiveMessage(consumer);
                    }
                }
            }
        }


        private static void ReceiveMessage(QueueingBasicConsumer consumer)
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();

            byte[] body = deliverEventArgs.Body;
            string message = Encoding.UTF8.GetString(body);

            Console.WriteLine("Received -> {0}", message);
        }
    }
}
