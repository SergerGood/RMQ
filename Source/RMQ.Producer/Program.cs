using System;

using RMQ.Core;
using RMQ.Core.Extensions;
using RMQ.Producer.Properties;

using RabbitMQ.Client;


namespace RMQ.Producer
{
    internal class Program
    {
        private static readonly string exchangeName = Settings.Default.ExchangeName;
        private static readonly string hostName = Settings.Default.HostName;
        private static readonly string queueName = Settings.Default.QueueName;

        private static void Main(string[] args)
        {
            CreateFanoutExchange();
        }

        private static void CreateDurableQueue()
        {
            using (var queue = new DurableQueue(hostName, queueName))
            {
                IModel channel = queue.GetChannel();

                Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    SendMessageToQueue(line, channel, queueName);
                }
            }
        }


        private static void CreateFanoutExchange()
        {
            using (var queue = new FanoutExchange(hostName, exchangeName))
            {
                IModel channel = queue.GetChannel();

                Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    SendMessageToExchange(line, channel, exchangeName);
                }
            }
        }


        private static void SendMessageToExchange(string text, IModel channel, string exchangeName)
        {
            var message = new Message(text);
            channel.SendPersistentMessageToExchange(exchangeName, message);

            Console.WriteLine("Sent -> {0}", message);
        }


        private static void SendMessageToQueue(string text, IModel channel, string queueName)
        {
            var message = new Message(text);
            channel.SendPersistentMessageToQueue(queueName, message);

            Console.WriteLine("Sent -> {0}", message);
        }
    }
}
