using System;
using System.Collections.Generic;

using RMQ.Consumer.Properties;
using RMQ.Core;
using RMQ.Core.ConsoleCommands;
using RMQ.Core.Extensions;

using RabbitMQ.Client;


namespace RMQ.Consumer
{
    internal class Program
    {
        private static readonly string hostName = Settings.Default.HostName;
        private static readonly string queueName = Settings.Default.QueueName;
        private static readonly string exchangeName = Settings.Default.ExchangeName;

        private static readonly List<ConsoleCommand> commands = new List<ConsoleCommand>
        {
            new ConsoleCommand { Argument = "/exchange", Default = true, Action = CreateFanoutExchange },
            new ConsoleCommand { Argument = "/queue", Action = CreateDurableQueue }
        };


        private static void Main(string[] args)
        {
            var executor = new CommandExecutor(commands, args);
            executor.Process();
        }


        private static void CreateDurableQueue()
        {
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


        private static void CreateFanoutExchange()
        {
            using (var queue = new FanoutExchange(hostName, exchangeName))
            {
                IModel channel = queue.GetChannel();
                channel.BindDurableQueue(queueName, exchangeName);

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
