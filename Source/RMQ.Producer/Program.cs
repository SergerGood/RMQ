using System;
using System.Text;

using RabbitMQ.Client;


namespace RMQ.Producer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

                    string line;
                    while ((line = Console.ReadLine()) != null)
                    {
                        byte[] body = Encoding.UTF8.GetBytes(line);

                        channel.BasicPublish("", "hello", null, body);

                        Console.WriteLine("Sent -> {0}", line);
                    }
                }
            }
        }
    }
}
