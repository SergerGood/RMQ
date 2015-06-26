using System;

using RabbitMQ.Client;


namespace RMQ.Core
{
    public sealed class FanoutExchange : Channel
    {
        public FanoutExchange(string hostName, string exchangeName)
            : base(hostName)
        {
            GetChannel().ExchangeDeclare(exchangeName, ExchangeType.Fanout);
        }
    }
}
