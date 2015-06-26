using System;

using RabbitMQ.Client;


namespace RMQ.Core
{
    public class Channel : IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;


        public Channel(string hostName)
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
