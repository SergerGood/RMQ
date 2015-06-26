using System;


namespace RMQ.Core
{
    public sealed class DurableQueue : Channel
    {
        public DurableQueue(string hostName, string queueName)
            : base(hostName)
        {
            GetChannel().QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}
