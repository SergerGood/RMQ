using System;

using RMQ.Consumer.Properties;
using RMQ.Core;
using RMQ.Core.Extensions;

using RabbitMQ.Client;


namespace RMQ.Consumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string hostName = Settings.Default.HostName;
            string queueName = Settings.Default.QueueName;

            using (var queue = new DurableQueue(hostName, queueName))
            {
                IModel channel = queue.GetChannel();
                QueueingBasicConsumer consumer = channel.CreateConsumerWithAck(queueName);

                Console.WriteLine("-> Waiting for messages. To exit press CTRL+C");

                while (true)
                {
                    ReceiveMessage(consumer, channel);
                }
            }
        }


        private static void ReceiveMessage(QueueingBasicConsumer consumer, IModel channel)
        {
            Message message = channel.ReceiveNextMessageWithAck(consumer);

            Console.WriteLine("Received -> {0}", message);
        }
    }
}
