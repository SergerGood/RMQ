using System;
using System.Text;

using RMQ.Producer.Properties;

using RabbitMQ.Client;


namespace RMQ.Producer
{
    internal class Program
    {
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

                    Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

                    string line;
                    while ((line = Console.ReadLine()) != null)
                    {
                        SendMessage(line, channel);
                    }
                }
            }
        }


        private static void SendMessage(string line, IModel channel)
        {
            byte[] body = Encoding.UTF8.GetBytes(line);

            channel.BasicPublish("", Settings.Default.QueueName, null, body);

            Console.WriteLine("Sent -> {0}", line);
        }
    }
}
