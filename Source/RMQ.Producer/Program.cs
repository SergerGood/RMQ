using System;

using RMQ.Core;
using RMQ.Core.Extensions;
using RMQ.Producer.Properties;

using RabbitMQ.Client;


namespace RMQ.Producer
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

                Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    SendMessage(line, channel, queueName);
                }
            }
        }


        private static void SendMessage(string text, IModel channel, string queueName)
        {
            var message = new Message(text);
            channel.SendPersistentMessage(queueName, message);

            Console.WriteLine("Sent -> {0}", message);
        }
    }
}
