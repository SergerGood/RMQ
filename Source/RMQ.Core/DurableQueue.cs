using System;

using RabbitMQ.Client;


namespace RMQ.Core
{
    public sealed class DurableQueue : IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;


        public DurableQueue(string hostName, string queueName)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(1)
            };

            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }


        public IModel GetChannel()
        {
            return channel;
        }


        public void Dispose()
        {
            if (channel != null)
            {
                channel.Close();
            }

            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
